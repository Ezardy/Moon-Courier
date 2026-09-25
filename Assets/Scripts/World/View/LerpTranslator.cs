using UnityEngine;

namespace Aniki.World {
	internal class LerpTranslator : MonoBehaviour {
		[SerializeField] private Transform			target;
		[SerializeField, Range(0, 1)] private float	progress = 0;

		private Vector3	startPosition;

		public float	Progress {
			get => progress;
			set => progress = value;
		}

		private void	Awake() {
			startPosition = transform.position;
		}

		private void	Update() {
			transform.position = Vector3.Lerp(startPosition, startPosition + target.position, progress);
		}
	}
}
