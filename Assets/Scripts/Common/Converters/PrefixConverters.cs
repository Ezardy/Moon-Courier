#if UNITY_EDITOR
using UnityEditor;

#else
using UnityEngine;
#endif
using UnityEngine.UIElements;

namespace Aniki.Common {
	public static class PrefixConverters {
	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	openingParenthesisGroup = new("Opening Parenthesis prefix");

			openingParenthesisGroup.AddConverter<string, string>(OpeningParenthesisConverter);

			ConverterGroups.RegisterConverterGroup(openingParenthesisGroup);
		}

		public static string	OpeningParenthesisConverter(ref string value) {
			return '(' + value;
		}
	}
}
