using Aniki.Common;
using Aniki.Common.Reactive;
using System;
using UnityEngine;

namespace Aniki.World {
	[RequireComponent(typeof(Camera))]
	internal class CameraResizer : MonoBehaviour {
		[SerializeField] private float									worldWidth = 20;
		[SerializeField] private Ref<IChannelSubscriber<Vector2Int>>	aspectSubscriber;
		[SerializeField] private Ref<IChannelPublisher<CameraSize>>	cameraSizePublisher;

		private Camera		camera;
		private IDisposable	disposable;

		private void	Awake() {
			camera = GetComponent<Camera>();
		}

		private void	OnEnable() {
			disposable = aspectSubscriber.I.Subscribe(Resize);
		}

		private void	OnDisable() {
			disposable.Dispose();
		}

		private void	Resize(Vector2Int size) {
			camera.orthographicSize = worldWidth * size.y / size.x / 2;
			cameraSizePublisher.I.Publish(new() {
				orthographicSize = camera.orthographicSize,
				aspect = camera.aspect
			});
		}
	}
}