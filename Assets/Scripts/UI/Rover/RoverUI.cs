using Aniki.Common;
using Aniki.Rover;
using System;
using UnityEngine;

namespace Aniki.UI {
	internal class RoverUI : MonoBehaviour {
		[SerializeField] private Dep<RoverViewModel>	viewModel;

		private IDisposable	disposable;

		private void	Start() {
			disposable = viewModel.Get(gameObject);
		}

		private void	OnDestroy() {
			disposable?.Dispose();
		}
	}
}
