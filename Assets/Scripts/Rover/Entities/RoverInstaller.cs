using Aniki.Common;
using UnityEngine;

namespace Aniki.Rover {
	public class RoverInstaller : SerializableMonoBehaviour {
		[SerializeField] private Dep<RoverModel>	model;

		public RoverModel	Model { get; private set; }

		private void	Awake() {
			Model = model.Get(gameObject);
		}

		private void	OnDestroy() {
			Model.Dispose();
		}
	}
}
