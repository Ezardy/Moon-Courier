using Aniki.Common;
using UnityEditor;
using UnityEngine;

namespace Aniki.Editor {
	[CustomPropertyDrawer(typeof(Dep<>), true)]
	internal class DepDrawer : PropertyDrawer {
		public override void	OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty	targetProp = property.FindPropertyRelative("target");
	
			EditorGUI.ObjectField(position, targetProp, label);
		}
	}
}
