﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.AppiumTests.Issues
{
	public class Issue22286 : _IssuesUITest
	{
		public Issue22286(TestDevice device)
			: base(device)
		{ }

		public override string Issue => "The iOS keyboard is not fully retracted and requires an extra click on the Done button";

		[Test]
		public void UpdateToolbarItemAfterNavigate()
		{
			this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.Android, TestDevice.Mac, TestDevice.Windows });

			App.WaitForElement("TestEditor1");
			App.Tap("TestEditor2");
			App.EnterText("TestEditor2", "abc");

			App.DismissKeyboard();

			VerifyScreenshot();
		}
	}
}