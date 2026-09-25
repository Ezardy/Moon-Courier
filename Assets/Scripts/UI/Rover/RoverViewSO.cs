using Aniki.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aniki.Rover {
	[CreateAssetMenu(fileName = "RoverView", menuName = "SO/Views/Rover")]
	internal class RoverViewSO : ADepSO<RoverView>{
		protected override RoverView	Create(GameObject container) {
			return new(container.GetComponent<PanelRenderer>());
		}
	}

	internal class RoverView {
		public VisualElement	Root => root;
		public Button			Cargo => cargo;
		public Button			Send => send;

		private VisualElement	root;
		private Button			cargo;
		private Button			send;

		public RoverView(PanelRenderer renderer) {
			renderer.RegisterUIReloadCallback(OnUIReload);
		}

		private void	OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version) {
			panelRenderer.UnregisterUIReloadCallback(OnUIReload);

			this.root = root;
			cargo  = root.Q<Button>("rover-controls__cargo");
			send = root.Q<Button>("rover-controls__send");
		}
	}
}
