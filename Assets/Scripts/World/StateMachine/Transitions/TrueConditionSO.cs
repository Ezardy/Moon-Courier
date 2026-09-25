using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Aniki.Common {
	[CreateAssetMenu(fileName = "TrueCondition", menuName = "State Machines/Conditions/True Condition")]
	internal class TrueConditionSO : StateConditionSO<TrueCondition> { }

	internal class TrueCondition : Condition {
		protected override bool	Statement() {
			return true;
		}
	}
}
