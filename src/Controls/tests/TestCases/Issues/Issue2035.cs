﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;

namespace Maui.Controls.Sample.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2035, "App crashes when setting CurrentPage on TabbedPage in ctor in 2.5.1pre1", PlatformAffected.Android)]
	public class Issue2035 : TestTabbedPage
	{
		const string Success = "Success";
		protected override void Init()
		{
			Title = "Bug";
			Children.Add(new ContentPage() { Title = "Page 1" });
			Children.Add(new ContentPage() { Title = "Page 2", Content = new Label { Text = Success } });
			Children.Add(new ContentPage() { Title = "Page 3" });
			CurrentPage = Children[1];
		}
	}
}