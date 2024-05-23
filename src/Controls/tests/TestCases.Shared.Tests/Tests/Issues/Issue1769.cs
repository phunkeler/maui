﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Issue1769 : _IssuesUITest
	{
		const string GoToPageTwoButtonText = "Go To Page 2";
		const string SwitchAutomatedId = nameof(SwitchAutomatedId);
		const string SwitchIsNowLabelTextFormat = "Switch is now {0}";

		public Issue1769(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "PushAsync with Switch produces NRE";
		public override bool ResetMainPage => false;

		[Test]
		[Category(UITestCategories.Switch)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnAndroid]
		[FailsOnIOS]
		public void Issue1769Test()
		{
			this.IgnoreIfPlatforms([TestDevice.Mac, TestDevice.Windows]);

			App.WaitForElement(GoToPageTwoButtonText);
			App.Tap(GoToPageTwoButtonText);

			App.WaitForElement(SwitchAutomatedId);
			App.WaitForElement(string.Format(SwitchIsNowLabelTextFormat, false));
			App.Tap(SwitchAutomatedId);
			App.WaitForElement(string.Format(SwitchIsNowLabelTextFormat, true));
		}
	}
}