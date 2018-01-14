using System;
using System.Linq;
using System.Text.RegularExpressions;
using ThreeInLine.Services;
using ThreeInLine.Services.Controllers;
using UnityEngine;

namespace Assets.Scripts
{
	public class BoardInputControllerComponent : BoardInputController
	{
		protected override void OnAwoke()
		{
			base.OnAwoke();

			Debug.Log("registering logger");
			Debug.RegisterLogger(CompositionRoot);
		}

#if UNITY_EDITOR
		private class GroupDesc
		{
			public int Index = -1;
			public Match Match;
			public Transform Transform;
		}

		// ReSharper disable once UnusedMember.Local
		private void Reset()
		{
			// setup pieces
			var pieceName = new Regex("(?<![\\S])piece_(?'index'[0-9]+)");
			var pieces = Transform
				.GetComponentsInChildren<Transform>(true)
				.Select(_ => new GroupDesc { Match = pieceName.Match(_.name), Transform = _ })
				.Where(_ => _.Match.Success)
				.ToArray();
			// min distance except 0f
			var size = Mathf.Pow(
				pieces
					.SelectMany(_ => pieces.Where(__ => !ReferenceEquals(_.Transform, __.Transform)).Select(__ => (_.Transform.position.ProjectXZ() - __.Transform.position.ProjectXZ()).sqrMagnitude))
					.Min(),
				.5f);
			Array.ForEach(pieces, _ =>
			{
				if(ExtractIndex(_.Match, out _.Index))
					SetupPiece(_.Transform, _.Index, size);
			});

			// setup selector
			var selectorName = new Regex("(?<![\\S])selector(?'index'_[0-9]*)?");
			var selectors = Transform
				.GetComponentsInChildren<Transform>(true)
				.Select(_ => new GroupDesc { Match = selectorName.Match(_.name), Transform = _ })
				.Where(_ => _.Match.Success)
				.ToArray();
			Array.ForEach(selectors, _ =>
			{
				if(ExtractIndex(_.Match, out _.Index))
					SetupSelector(_.Transform, _.Index, size);
			});
		}

		private bool ExtractIndex(Match match, out int index)
		{
			index = -1;
			try
			{
				index =
					match.Success && match.Groups["index"].Success
						? Convert.ToInt32(match.Groups["index"].Value)
						: -1;
				return match.Success;
			}
			catch
			{
				return false;
			}
		}

		private void SetupPiece(Transform transform, int index, float size)
		{
			if(transform.GetComponent<PieceControllerComponent>() == null)
			{
				var component = transform.gameObject.AddComponent<PieceControllerComponent>();
				component.SetIndex(index);

			}
			if(transform.GetComponent<Collider>() == null)
			{
				var collider = transform.gameObject.AddComponent<BoxCollider>();
				collider.size = new Vector3(size, .05f, size);
			}
		}

		private void SetupSelector(Transform transform, int index, float size)
		{
			if(transform.GetComponent<SelectorControllerComponent>() == null)
			{
				transform.gameObject.AddComponent<SelectorControllerComponent>();
			}
		}
#endif
	}
}