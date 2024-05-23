﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;

namespace Maui.Controls.Sample.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Bugzilla, 44476, "[Android] Unwanted margin at top of details page when nested in a NavigationPage")]
	public class Bugzilla44476 : TestNavigationPage
	{
		protected override void Init()
		{
			BackgroundColor = Colors.Maroon;
			PushAsync(new FlyoutPage
			{
				Title = "Bugzilla Issue 44476",
				Flyout = new ContentPage
				{
					Title = "Flyout",
					Content = new StackLayout
					{
						Children =
						{
							new Label { Text = "Flyout" }
						}
					}
				},
				Detail = new ContentPage
				{
					Title = "Detail",
					Content = new StackLayout
					{
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children =
						{
							new Label { Text = "Detail Page" },
							new StackLayout
							{
								VerticalOptions = LayoutOptions.EndAndExpand,
								Children =
								{
									new Label { Text = "This should be visible." }
								}
							}
						}
					}
				},
			});
		}
	}
}