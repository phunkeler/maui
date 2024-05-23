﻿#if IOS
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Bugzilla31688 : _IssuesUITest
	{
		public Bugzilla31688(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "[Bug] Exception Ancestor must be provided for all pushes except first";
		public override bool ResetMainPage => false;

		[Test]
		[Category(UITestCategories.Navigation)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnAndroid]
		[FailsOnIOS]
		public void Bugzilla31688Test()
		{
			App.WaitForNoElement("Page3");
		}
	}
}
#endif