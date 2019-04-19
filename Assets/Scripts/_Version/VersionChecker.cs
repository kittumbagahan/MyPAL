using UnityEngine;

namespace _Version
{
	public class VersionChecker{

		public static string Version()
		{
			return PlayerPrefs.GetString("version", "0.0.9");
		}

		public static bool IsNewVersionGreater(string newVersion)
		{						
			var currentVersionArray = Version().Split('.');
			var versionArray = newVersion.Split('.');

			for (int index = 0; index < currentVersionArray.Length; index++)
			{				
				return int.Parse(currentVersionArray[index]) < int.Parse(versionArray[index]) ? true : false;
			}

			return false;
		}				
	}
}
