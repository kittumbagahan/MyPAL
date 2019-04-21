using System.Linq;
using UnityEngine;

namespace _Version
{
	public static class VersionChecker{

		public static string Version()
		{
			return PlayerPrefs.GetString("version", "0.0.9");
		}

		public static bool IsNewVersionGreater(string newVersion)
		{						
			var currentVersionArray = Version().Split('.');
			var versionArray = newVersion.Split('.');

			return currentVersionArray.Select((t, index) => int.Parse(t) < int.Parse(versionArray[index])).FirstOrDefault();
		}

		public static void SetNewVersion(string newVersion)
		{
			PlayerPrefs.SetString("version", newVersion);
		}
	}
}
