using UnityEngine;

namespace _Version
{
	public class VersionChecker{

		public static string Version()
		{
			return PlayerPrefs.GetString("version", "1.0.0");
		}

		public static bool IsCurrentVersionGreater(string version)
		{
			
			
			return true;
		}

		public string GetGreaterVersion(string currentVersion, string version)
		{
			var currentVersionArray = currentVersion.Split('.');
			var versionArray = version.Split('.');

			for (int index = 0; index < currentVersionArray.Length; index++)
			{				
				return int.Parse(currentVersionArray[index]) > int.Parse(versionArray[index]) ? currentVersion : version;
			}

			return currentVersion;
		}				
	}
}
