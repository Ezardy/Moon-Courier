using Aniki.Common;
using Aniki.Common.Reactive;
using UnityEngine;

namespace Aniki.World {
	internal class ScreenSizeObserver : MonoBehaviour {
		[SerializeField] private Ref<IChannelPublisher<Vector2Int>>	sizePublisher;

		private int	width = 0;
		private int	height = 0;

		private void	Update() {
			if (Screen.width != width || Screen.height != height) {
				width = Screen.width;
				height = Screen.height;
				sizePublisher.I.Publish(new(width, height));
			}
		}
	}
}
