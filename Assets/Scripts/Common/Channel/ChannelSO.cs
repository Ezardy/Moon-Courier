using System;
using UnityEngine;

namespace Aniki.Common {
	[CreateAssetMenu(fileName = "Channel", menuName = "Channels/Channel")]
	internal class ChannelSO : ScriptableObject, IChannel {
		private readonly Channel	channel = new();

		public void	Publish() {
			channel.Publish();
		}

		public IDisposable	Subscribe(Action action) {
			return channel.Subscribe(action);
		}
	}
}
