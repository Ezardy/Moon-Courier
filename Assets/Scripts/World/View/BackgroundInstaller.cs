using Aniki.Common;
using Aniki.Common.Reactive;
using System;
using UnityEngine;

namespace Aniki.World {
	internal class BackgroundInstaller : MonoBehaviour {
		[SerializeField] private SpriteRenderer	ground;
		[SerializeField] private SpriteRenderer	sky;

		[SerializeField] private Ref<IChannelSubscriber<CameraSize>>	cameraSizeSubscriber;

		private IDisposable	disposable;

		private void	OnEnable() {
			disposable = cameraSizeSubscriber.I.Subscribe(Install);
		}

		private void	OnDisable() {
			disposable.Dispose();
		}

		private void	Install(CameraSize size) {
			float	groundHeight = ground.size.y;
			float	width = size.orthographicSize * size.aspect * 2;

			ground.size = new(width, groundHeight);
			ground.transform.position = new(0, -size.orthographicSize,0);

			sky.size = new(width, size.orthographicSize * 2 - groundHeight);
			sky.transform.position = new(0, groundHeight - size.orthographicSize, 0);
		}
	}
}