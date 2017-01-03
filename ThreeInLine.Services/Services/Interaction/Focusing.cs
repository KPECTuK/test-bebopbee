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

		private readonly IContainer _container;
		private readonly RaycastHit[] _hits = new RaycastHit[SCAN_DEEPNESS_I];
		private IPieceController _inFocus;

		public int InFocusIndex => _inFocus?.Index ?? -1;
		public IPieceController InFocus => _inFocus;

		public Focusing(IContainer container)
		{
			_container = container;
		}
		
		public void Scan(Ray ray, bool isIntent)
		{
			if(!isIntent)
				return;

			var last = _inFocus;
			_inFocus = null;

			Physics.RaycastNonAlloc(ray, _hits);
			Array.ForEach(_hits, _ => _inFocus = _inFocus ?? _.transform?.GetComponent<IPieceController>());

			if(!ReferenceEquals(last, _inFocus))
			{
				// onFocus Change
				Container.Container.CompositionRoot.Resolve<ILog>().Log(GetType(), Level.Debug, $"selection changed to: {_inFocus?.Index ?? -1} with ray: {ray}");
			}
		}
	}
}
