using Aniki.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aniki.UI {
	[CreateAssetMenu(fileName = "MainView", menuName = "SO/Views/Main")]
	internal class MainViewSO : ASingleDepSO<MainView> {
		protected override MainView	Create(GameObject container) {
			return new(container.GetComponent<PanelRenderer>());
		}
	}

	internal class MainView {
		public bool	IsReady { get; private set; }


		public VisualElement	Root { get; private set; }

		public Button	Map { get; private set; }
		public Button	CloseInventory { get; private set; }
		public Button	CloseMap { get; private set; }

		public MainView(PanelRenderer panelRenderer) {
			panelRenderer.RegisterUIReloadCallback(OnGUIReload);
		}

		private void	OnGUIReload(PanelRenderer panelRenderer, VisualElement root, int version) {
			panelRenderer.UnregisterUIReloadCallback(OnGUIReload);

			Root = root;

			Map = root.Q<Button>("base-screen__map");
			CloseInventory = root.Q<Button>("inventory-screen__close");
			CloseMap = root.Q<Button>("map-screen__close");

			IsReady = true;
		}
	}
}
