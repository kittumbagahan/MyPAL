using NUnit.Framework;
using UnityEngine;
using _Version;

namespace Editor.Tests
{
	public class VersionCheckerTests 
	{
		[Test]
		public void IsNewVersionGreater_CheckVersion_ReturnNewerVersion()// version is: 1.0.0
		{
			PlayerPrefs.DeleteKey("version");

			var currentVersion = VersionChecker.Version();
			var newVersion = VersionChecker.IsNewVersionGreater("1.0.0");						
						
			Assert.That(newVersion, Is.EqualTo(true));
		}
	}
}
