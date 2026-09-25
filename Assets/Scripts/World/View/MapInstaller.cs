using Aniki.Common;
using Aniki.Common.Reactive;
using System;
using UnityEngine;

namespace Aniki.World {
	[RequireComponent(typeof(SpriteRenderer))]
	internal class MapInstaller : MonoBehaviour {
		[SerializeField] private Ref<IChannelSubscriber<CameraSize>>	cameraSizeSubscriber;

		private SpriteRenderer	renderer;
		private IDisposable		disposable;

		private float	aspect;

		private void	Awake() {
			renderer = GetComponent<SpriteRenderer>();
			aspect = renderer.size.x / renderer.size.y;
		}

		private void	OnEnable() {
			disposable = cameraSizeSubscriber.I.Subscribe(Install);
		}

		private void	OnDisable() {
			disposable.Dispose();
		}

		private void	Install(CameraSize size) {
			float	screenHeight = size.orthographicSize * 2;
			float	screenWidth = screenHeight * size.aspect;

			renderer.size = new(screenHeight * aspect, screenHeight);
			if (renderer.size.x > screenWidth)
				renderer.size = new(screenWidth, screenWidth / aspect);

			renderer.transform.position = new(0, -screenHeight, 0);
		}
	}
}
