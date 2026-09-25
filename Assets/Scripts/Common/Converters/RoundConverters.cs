#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif
using UnityEngine.UIElements;

namespace Aniki.Common {
	public static class RoundConverters {
	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	f1Group = new("F1");
			ConverterGroup	f2Group = new("F2");

			f1Group.AddConverter<float, string>(F1Converter);
			f2Group.AddConverter<float, string>(F2Converter);

			ConverterGroups.RegisterConverterGroup(f1Group);
			ConverterGroups.RegisterConverterGroup(f2Group);
		}

		public static string	F1Converter(ref float value) {
			return value.ToString("F1");
		}

		public static string	F2Converter(ref float value) {
			return value.ToString("F2");
		}
	}
}
