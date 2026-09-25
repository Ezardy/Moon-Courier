using Unity.Properties;
using UnityEngine;

namespace Aniki.World {
	[CreateAssetMenu(fileName = "TimeUntilImpact", menuName = "SO/Models/Time Until Impact")]
	public class TimeUntilImpactSO : ScriptableObject {
		[SerializeField, Min(0)] private float	timeUntilImpact;

		private float	remainingTime;

		[CreateProperty] public float	RemainingTime => remainingTime;

		public float	TimeUntilImpact => timeUntilImpact;

		public void	Elapse(float time) {
			remainingTime = Mathf.Max(0, remainingTime - time);
		}

		public void	ResetTime() {
			remainingTime = timeUntilImpact;
		}
	}
}
