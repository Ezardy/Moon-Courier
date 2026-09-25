using Aniki.Common;
using Aniki.Parts;
using UnityEngine;
using UnityEngine.Splines;

namespace Aniki.Path {
	[CreateAssetMenu(fileName = "PathModel", menuName = "SO/Models/Path")]
	internal class PathModelSO : ASingleDepSO<PathModel> {
		[SerializeField, Min(0)] private float			distance;
		[SerializeField, Min(0)] private float			risk;
		[SerializeField] private Dep<IPartManipulator>	destination;

		public float	Distance => distance;
		public float	Risk => risk;

		protected override PathModel	Create(GameObject container) {
			return new(this,
				container.GetComponent<SplineContainer>(),
				destination.Get(container.GetComponent<PathBridge>().Destination));
		}
	}

	public class PathModel {
		private readonly PathModelSO	sourceSO;

		public SplineContainer	Spline { get; private set; }
		public IPartManipulator	Destination { get; private set; }

		public float	Distance => sourceSO.Distance;
		public float	Risk => sourceSO.Risk;

		internal PathModel(PathModelSO sourceSO, SplineContainer spline, IPartManipulator destination) {
			this.sourceSO = sourceSO;
			Spline = spline;
			Destination = destination;
		}
	}
}
