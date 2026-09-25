using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aniki.Common {
	public interface IDep<T> {
		public T	Get(GameObject container = null);
	}

	[System.Serializable]
	public class Dep<T> : IDep<T>, ISerializationCallbackReceiver {
		[SerializeField] private Object	target;

		public T	Get(GameObject container = null) {
			return target is T dep ? dep : ((IDep<T>)target).Get(container);
		}

		public void	OnAfterDeserialize() { }

		public void	OnBeforeSerialize() {
			if (target is not T && target is not IDep<T>)
				target = null;
		}
	}

	public abstract class ADepSO<T> : SerializableScriptableObject, IDep<T> {
		public virtual T	Get(GameObject container = null) {
			return Create(container);
		}

		protected abstract T	Create(GameObject container);
	}

	public abstract class ASingleDepSO<T> : ADepSO<T> {
		private T	instance;

		public override T	Get(GameObject container) {
			instance ??= Create(container);
			return instance;
		}

	#if UNITY_EDITOR
		private void	Reset() {
			instance = default;
		}

		private void	OnEnable() {
			EditorApplication.playModeStateChanged += OnPlayModeChanged;
		}

		private void	OnDisable() {
			EditorApplication.playModeStateChanged -= OnPlayModeChanged;
		}

		private void	OnPlayModeChanged(PlayModeStateChange change) {
			if (change == PlayModeStateChange.ExitingPlayMode)
				Reset();
		}
	#endif
	}

	public abstract class APooledDepSO<T> : ADepSO<T> {
		protected virtual Dictionary<GameObject, T>	Instances { get; private set; } = new();

		public override T	Get(GameObject container = null) {
			if (!Instances.TryGetValue(container, out T instance)) {
				instance = Create(container);
				Instances.Add(container, instance);
			}
			return instance;
		}

	#if UNITY_EDITOR
		private void	Reset() {
			Instances.Clear();
		}

		private void	OnEnable() {
			EditorApplication.playModeStateChanged += OnPlayModeChanged;
		}

		private void	OnDisable() {
			EditorApplication.playModeStateChanged -= OnPlayModeChanged;
		}

		private void	OnPlayModeChanged(PlayModeStateChange change) {
			if (change == PlayModeStateChange.ExitingPlayMode)
				Reset();
		}
	#endif
	}
}
