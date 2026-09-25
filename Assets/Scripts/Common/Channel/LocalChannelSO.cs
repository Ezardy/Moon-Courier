using UnityEngine;

namespace Aniki.Common {
	[CreateAssetMenu(fileName = "LocalChannel", menuName = "Channels/Local/Channel")]
	public class LocalChannelSO : APooledDepSO<IChannel> {
		protected override IChannel	Create(GameObject container) {
			return new Channel();
		}
	}

	public abstract class ALocalChannelSO<T> : APooledDepSO<IChannel<T>> {
		protected override IChannel<T>	Create(GameObject container) {
			return new Channel<T>();
		}
	}
}
