using System;
using ThreeInLine.Services.Container;
using ThreeInLine.Services.Controllers;
using ThreeInLine.Services.Logging;
using UnityEngine;

namespace ThreeInLine.Services.Interaction
{
	internal class Focusing : IFocusing
	{
		private const int SCAN_DEEPNESS_I = 3;
		private const float DOUBLECLICK_INTERVAL_F = .7f;
		private const float DRAGSTOP_INTERVAL_F = 1f;

		//private readonly IContainer _container;
		private readonly RaycastHit[] _hits = new RaycastHit[SCAN_DEEPNESS_I];
		private int _clicksOverTheSame;
		private float _lastClickTime;
		private float _lastDragTime;
		private bool _isDragInProgress;

		public int InFocusIndex { get { return InFocus == null ? -1 : InFocus.Index; } }
		public IPieceController InFocus { get; private set; }
		public IPieceController SwipedTo { get; private set; }
		public bool WasDoubleClick { get; private set; }
		public bool WasSwipe { get; private set; }

		public Focusing(IContainer container)
		{
			//_container = container;
		}

		public void MaintainSwipe(Ray ray, bool isAction)
		{
			_isDragInProgress = isAction && _lastClickTime < DRAGSTOP_INTERVAL_F;
			_lastDragTime = isAction ? Time.realtimeSinceStartup : _lastDragTime;

			if(ReferenceEquals(InFocus, null) || !isAction)
				return;

			var swipedTo = SwipedTo;
			SwipedTo = null;

			Physics.RaycastNonAlloc(ray, _hits);
			Array.ForEach(_hits, _ => SwipedTo = SwipedTo ?? (_.transform == null ? null : _.transform.GetComponent<IPieceController>()));

			WasSwipe =
				!ReferenceEquals(SwipedTo, swipedTo) &&
				!ReferenceEquals(InFocus, SwipedTo);

			if(WasSwipe)
				this.Log(Level.Debug, "WAS SWIPE TO: " + (SwipedTo == null ? -1 : SwipedTo.Index));
		}

		public void MaintainClick(Ray ray, bool isAction)
		{
			WasDoubleClick = false;
			if(!isAction || _isDragInProgress)
				return;

			var last = InFocus;
			InFocus = null;

			Physics.RaycastNonAlloc(ray, _hits);
			Array.ForEach(_hits, _ => InFocus = InFocus ?? (_.transform == null ? null : _.transform.GetComponent<IPieceController>()));

			if(!ReferenceEquals(last, InFocus))
				this.Log(Level.Notice, "SELECTION CHANGED TO: " + (InFocus == null ? -1 : InFocus.Index));

			_clicksOverTheSame =
				!ReferenceEquals(last, null) &&
				ReferenceEquals(last, InFocus) &&
				Time.realtimeSinceStartup - _lastClickTime < DOUBLECLICK_INTERVAL_F
					? _clicksOverTheSame + 1
					: 0;
			WasDoubleClick = _clicksOverTheSame > 1;
			_clicksOverTheSame = _clicksOverTheSame > 1 ? 0 : _clicksOverTheSame;
			_lastClickTime = Time.realtimeSinceStartup;

			if(WasDoubleClick)
				this.Log(Level.Debug, "DOUBLE CLICK OVER: " + (InFocus == null ? -1 : InFocus.Index));
		}
	}
}
