using NUnit.Framework;
using UnityEngine;
using _Version;

namespace Editor.Tests
{
	public class VersionCheckerTests 
	{
		[Test]
		public void Version_DefaultVersion_ReturnsFirstVersion()// version is: 1.0.0
		{
			PlayerPrefs.DeleteKey("version");

			var result = VersionChecker.Version();
			
			Assert.That(result, Is.EqualTo("1.0.0"));
		}

		[Test]
		public void GetGreaterVersion_CompareToGreaterVersion_ReturnGreaterVersion()
		{
			var currentVersion = "1.0.0";
			var newerVersion = "1.0.1";
			
			var versionChecker = new VersionChecker();

			var result = versionChecker.GetGreaterVersion(currentVersion, newerVersion);
			
			Assert.That(result, Is.EqualTo("1.0.1"));
		}
	}
}
