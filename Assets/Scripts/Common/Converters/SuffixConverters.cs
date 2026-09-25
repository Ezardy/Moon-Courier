#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif
using UnityEngine.UIElements;

namespace Aniki.Common {
	public static class SuffixConverters {
	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	closingParenthesisGroup = new("Closing Parenthesis prefix");
			ConverterGroup	perKmGroup = new("Per Km Suffix");

			closingParenthesisGroup.AddConverter<string, string>(ClosingParenthesisConverter);
			perKmGroup.AddConverter<string, string>(PerKmConverter);

			ConverterGroups.RegisterConverterGroup(closingParenthesisGroup);
			ConverterGroups.RegisterConverterGroup(perKmGroup);
		}

		public static string	ClosingParenthesisConverter(ref string value) {
			return value + ')';
		}

		public static string	PerKmConverter(ref string value) {
			return value + "/km";
		}
	}
}
