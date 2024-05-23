﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;

namespace Maui.Controls.Sample.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 53179,
		"PopAsync crashing after RemovePage when support packages are updated to 25.1.1", PlatformAffected.Android)]
	public class Bugzilla53179 : TestNavigationPage
	{

		class TestPage : ContentPage
		{
			Button nextBtn, rmBtn, popBtn;

			public TestPage(int index)
			{
				nextBtn = new Button { Text = "Next Page" };
				rmBtn = new Button { Text = "Remove previous pages" };
				popBtn = new Button { Text = "Back" };

				nextBtn.Clicked += async (sender, e) => await Navigation.PushAsync(new TestPage(index + 1));
				rmBtn.Clicked += (sender, e) =>
				{
					var stackSize = Navigation.NavigationStack.Count;
					Navigation.RemovePage(Navigation.NavigationStack[stackSize - 2]);

					stackSize = Navigation.NavigationStack.Count;
					Navigation.RemovePage(Navigation.NavigationStack[stackSize - 2]);

					popBtn.IsVisible = true;
					rmBtn.IsVisible = false;
				};
				popBtn.Clicked += async (sender, e) => await Navigation.PopAsync();

				switch (index)
				{
					case 4:
						nextBtn.IsVisible = false;
						popBtn.IsVisible = false;
						break;
					default:
						rmBtn.IsVisible = false;
						popBtn.IsVisible = false;
						break;
				}

				Content = new StackLayout
				{
					Children = {
					new Label { Text = $"This is page {index}"},
					nextBtn,
					rmBtn,
					popBtn
				}
				};
			}
		}


		protected override void Init()
		{
			PushAsync(new TestPage(1));
		}
	}
}