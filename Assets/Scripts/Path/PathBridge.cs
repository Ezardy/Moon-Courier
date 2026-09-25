using UnityEngine;

namespace Aniki.Path {
	internal class PathBridge : MonoBehaviour {
		[SerializeField] private GameObject	destination;

		public GameObject	Destination => destination;
	}
}
