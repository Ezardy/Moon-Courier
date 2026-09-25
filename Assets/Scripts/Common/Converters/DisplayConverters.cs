#if UNITY_EDITOR
using UnityEditor;

#else
using UnityEngine;
#endif
using UnityEngine.UIElements;

namespace Aniki.Common {
	internal static class DisplayConverters {
	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	displayGroup = new("Display");

			displayGroup.AddConverter<bool, StyleEnum<DisplayStyle>>(BoolToDisplayStyleConverter);

			ConverterGroups.RegisterConverterGroup(displayGroup);
		}

		private static StyleEnum<DisplayStyle>	BoolToDisplayStyleConverter(ref bool isShown) {
			return isShown ? DisplayStyle.Flex : DisplayStyle.None;
		}
	}
}
