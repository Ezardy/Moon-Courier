using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "LasersPower", menuName = "SO/Models/Lasers Power")]
internal class LasersPowerSO : ScriptableObject {
	[SerializeField, Min(0)] private int	goalPower;

	private int	power;

	public void	Add(int power) {
		this.power = Mathf.Min(goalPower, this.power + power);
	}

	[CreateProperty] public int	Power => power;
}
