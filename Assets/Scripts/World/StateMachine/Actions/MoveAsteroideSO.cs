using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Aniki.World {
	[CreateAssetMenu(fileName = "MoveAsteroide", menuName = "SO/Actions/Move Asteroide")]
	internal class MoveAsteroideSO : StateActionSO {
		[SerializeField] private TimeUntilImpactSO	timeUntilImpact;

		public TimeUntilImpactSO	TimeUntilImpact => timeUntilImpact;

		protected override StateAction	CreateAction() => new MoveAsteroide();
	}

	internal class MoveAsteroide : StateAction {
		protected new MoveAsteroideSO	OriginSO => (MoveAsteroideSO)base.OriginSO;

		public override void	OnUpdate() {
			OriginSO.TimeUntilImpact.Elapse(Time.deltaTime);
		}
	
		public override void	OnStateEnter() {
			OriginSO.TimeUntilImpact.ResetTime();
		}
	}
}
