using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using AndroidX.Activity;
using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using AndroidUri = Android.Net.Uri;

namespace Microsoft.Maui.ApplicationModel
{
	static class PickVisualMediaForResult
	{
		static ActivityResultLauncher launcher;
		static TaskCompletionSource<AndroidUri> tcs = null;

		public static void Register(ComponentActivity componentActivity)
		{
			// If "Old", register "LegacyPickVisual
			// TODO: Discover why "IsPhotoPickerAvailable" was deprecated and 
			var contract = ActivityResultContracts.PickVisualMedia.InvokeIsPhotoPickerAvailable(Application.Context)
				? new ActivityResultContracts.PickVisualMedia()
				: new MauiLegacyPickVisualMediaContract();
			var callback = new ActivityResultCallback<AndroidUri>(uri => tcs?.SetResult(uri));
			launcher = componentActivity.RegisterForActivityResult(contract, callback);
		}

		public static Task<AndroidUri> Launch(PickVisualMediaRequest request)
		{
			tcs = new TaskCompletionSource<AndroidUri>();

			if (launcher is null)
			{
				tcs.SetCanceled();
				return tcs.Task;
			}

			try
			{
				launcher.Launch(request);
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}

			return tcs.Task;
		}

		[Obsolete($"Please use {nameof(ActivityResultContracts.PickVisualMedia)} instead.")]
		public class MauiLegacyPickVisualMediaContract : ActivityResultContract
		{
			public override Intent CreateIntent(Context context, Java.Lang.Object input)
			{
				throw new NotImplementedException();
			}

			public override Java.Lang.Object ParseResult(int resultCode, Intent intent)
			{
				throw new NotImplementedException();
			}
		}

	}
}
