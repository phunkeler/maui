using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

namespace Microsoft.Maui
{
	internal class MauiActivityResultObserver : IDefaultLifecycleObserver
	{

	}

	public partial class MauiAppCompatActivity : AppCompatActivity
	{
		// Override this if you want to handle the default Android behavior of restoring fragments on an application restart
		protected virtual bool AllowFragmentRestore => false;

		protected override void OnCreate(Bundle? savedInstanceState)
		{
			if (!AllowFragmentRestore)
			{
				// This appears to be legacy Xamarin code.
				// Debugging the latest net8.0 samples show
				// that these keys no longer exist on "savedInstanceState.
				// Caveat:
				//	1. Consumers are free to modify the SavedStateRegistry as part of their app's lifecycle.
				// Todo:
				//	1. Determine the environmental conditions under which these are set (OS-Version/API-Level, MAUI version, etc...)
				//	2. Once defined, conditionally implement https://github.com/dotnet/maui/issues/14037#issuecomment-2274054948
				//	3. Write device/UI tests demonstrating functionality
				//		- How do I run UI tests?
				//		- How do I run Device Tests?

				savedInstanceState?.Remove("android:support:fragments");
				savedInstanceState?.Remove("androidx.lifecycle.BundlableSavedStateRegistry.key");
				savedInstanceState?
					.GetBundle("androidx.lifecycle.BundlableSavedStateRegistry.key")?
					.Remove("android:support:fragments");
			}

			// If the theme has the maui_splash attribute, change the theme
			if (Theme.TryResolveAttribute(Resource.Attribute.maui_splash))
			{
				SetTheme(Resource.Style.Maui_MainTheme_NoActionBar);
			}

			base.OnCreate(savedInstanceState);

			if (IPlatformApplication.Current?.Application is not null)
			{
				this.CreatePlatformWindow(IPlatformApplication.Current.Application, savedInstanceState);
			}
		}

		public override bool DispatchTouchEvent(MotionEvent? e)
		{
			// For current purposes this needs to get called before we propagate
			// this message out. In Controls this dispatch call will unfocus the 
			// current focused element which is important for timing if we should
			// hide/show the softkeyboard.
			// If you move this to after the xplat call then the keyboard will show up
			// then close
			bool handled = base.DispatchTouchEvent(e);

			bool implHandled =
				(this.GetWindow() as IPlatformEventsListener)?.DispatchTouchEvent(e) == true;

			return handled || implHandled;
		}
	}
}