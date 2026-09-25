using UnityEngine;

namespace Aniki.World {
	internal class LerpScaler : MonoBehaviour {
		[SerializeField] private Vector3			target;
		[SerializeField, Range(0, 1)] private float	progress;

		private Vector3	initialScale;

		public float	Progress {
			get => progress;
			set => progress = value;
		}

		private void	Awake() {
			initialScale = transform.localScale;
		}

		private void	Update() {
			transform.localScale = Vector3.Lerp(initialScale, target, progress);
		}
	}
}
