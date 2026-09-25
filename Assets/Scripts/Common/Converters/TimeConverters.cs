#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif
using System.Text;
using UnityEngine.UIElements;

namespace Aniki.Common {
	public static class TimeConverters {
	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	group = new("Time");

			group.AddConverter<float, string>(SecondsToHHMMSSConverter);

			ConverterGroups.RegisterConverterGroup(group);
		}

		public static string	SecondsToHHMMSSConverter(ref float time) {
			float			t = time;
			int				hours = (int)(t / 3600);
			int				minutes;
			int				seconds;
			StringBuilder	str = new(8);

			t -= hours * 3600;
			minutes = (int)(t / 60);
			t -= minutes * 60;
			seconds = (int)t;
			if (hours > 0)
				str.AppendFormat("{0:D2}:", hours);
			str.AppendFormat("{0:D2}:{1:D2}", minutes, seconds);
			return str.ToString();
		}
	}
}
