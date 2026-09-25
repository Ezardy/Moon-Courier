using Aniki.Common;
using System;
using UnityEngine;

namespace Aniki.UI {
	internal class MainUI : MonoBehaviour {
		[SerializeField] private Dep<MainViewModel>	viewModel;

		private IDisposable	disposable;

		private void	Start() {
			disposable = viewModel.Get(gameObject);
		}

		private void	OnDestroy() {
			disposable?.Dispose();
		}
	}
}
