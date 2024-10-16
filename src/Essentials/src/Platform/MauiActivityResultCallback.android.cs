using System;
using System.Threading.Tasks;
using AndroidX.Activity.Result;
using JavaObject = Java.Lang.Object;

namespace Microsoft.Maui.ApplicationModel
{
	class ActivityResultCallback<T> : JavaObject, IActivityResultCallback
	{
		readonly Action<T> _callback;

		public ActivityResultCallback(Action<T> callback) => _callback = callback;
		public ActivityResultCallback(TaskCompletionSource<T> tcs) => _callback = tcs.SetResult;

		public void OnActivityResult(JavaObject result)
		{
			if (result is T obj)
			{
				_callback(obj);
			}
		}
	}
}