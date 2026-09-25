using UnityEngine;

namespace Aniki.World {
	[RequireComponent(typeof(LerpScaler))]
	internal class AsteroideMover : MonoBehaviour {
		[SerializeField] private TimeUntilImpactSO	timeUntilImpact;

		private LerpScaler	scaler;

		private void	Awake() {
			scaler = GetComponent<LerpScaler>();
		}

		private void	Update() {
			scaler.Progress = 1 - timeUntilImpact.RemainingTime / timeUntilImpact.TimeUntilImpact;
		}
	}
}
