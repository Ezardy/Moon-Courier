using Aniki.Common;
using UnityEngine;

namespace Aniki.World {
	[CreateAssetMenu(fileName = "CameraSizeChannel", menuName = "Channels/Camera Size Channel")]
	internal class CameraSizeChannel : AChannel<CameraSize> { }

	internal struct CameraSize {
		public float	orthographicSize;
		public float	aspect;
	}
}
