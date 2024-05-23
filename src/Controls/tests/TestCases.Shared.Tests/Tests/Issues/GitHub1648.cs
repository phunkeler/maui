﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class GitHub1648 : _IssuesUITest
	{
		public GitHub1648(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "FlyoutPage throws ArgumentOutOfRangeException";
		public override bool ResetMainPage => false;

		[Test]
		[Category(UITestCategories.Navigation)]
		[Category(UITestCategories.Compatibility)]
		public void GitHub1648Test()
		{
			this.IgnoreIfPlatforms([TestDevice.Android, TestDevice.iOS, TestDevice.Mac]);

			App.WaitForElement("Reload");
			App.Tap("Reload");
			App.WaitForElement("Success");
		}
	}
}