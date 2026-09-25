using UnityEngine;

namespace Aniki.Common {
	public class SerializableMonoBehaviour : MonoBehaviour, IIdentifiable {
		[SerializeField, HideInInspector] private string	guid;
		public string	GUID => guid;
	
	#if UNITY_EDITOR
		private void	OnValidate() {
			if (string.IsNullOrEmpty(guid))
				guid = System.Guid.NewGuid().ToString();
		}
	#endif
	}
}
