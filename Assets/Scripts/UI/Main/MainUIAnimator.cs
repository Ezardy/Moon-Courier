using UnityEngine;
using Aniki.Common;
using System;

namespace Aniki.UI {
	[RequireComponent(typeof(Animator))]
	internal class MainUIAnimator : MonoBehaviour {
		private static readonly int	isMapHash = Animator.StringToHash("IsMap");

		[Header("Publishers")]

		[SerializeField] private Ref<IChannelPublisher>	mapOpened;
		[SerializeField] private Ref<IChannelPublisher>	mapClosed;

		[Header("Subscribers")]

		[SerializeField] private Ref<IChannelSubscriber>	startOpenMap;
		[SerializeField] private Ref<IChannelSubscriber>	startCloseMap;

		private Animator	animator;
		private IDisposable	disposable;

		private void	Awake() {
			animator = GetComponent<Animator>();

			IDisposable	d1 = startOpenMap.I.Subscribe(StartOpenMap);
			IDisposable	d2 = startCloseMap.I.Subscribe(StartCloseMap);

			disposable = new Disposable(d1, d2);
		}

		private void	OnDestroy() {
			disposable.Dispose();
		}

		private void	StartOpenMap() {
			animator.SetBool(isMapHash, true);
		}

		private void	StartCloseMap() {
			animator.SetBool(isMapHash, false);
		}

		private void	OpenMap() {
			mapOpened.I.Publish();
		}

		private void	CloseMap() {
			mapClosed.I.Publish();
		}
	}
}
