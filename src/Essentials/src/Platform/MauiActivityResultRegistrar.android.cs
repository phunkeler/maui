#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AndroidX.Activity;
using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using AndroidUri = Android.Net.Uri;
using JavaObject = Java.Lang.Object;

namespace Microsoft.Maui.ApplicationModel;

/// <summary>
/// "the callback must be unconditionally registered every time your activity is created" -- https://developer.android.com/training/basics/intents/result#register
/// Meaning, this should always be tied to "MauiAppCompatActivity's "OnCreate".
/// </summary>
/// <param name="componentActivity"></param>
internal class MauiActivityResultRegistrar(ComponentActivity componentActivity)
{
	private static readonly ConcurrentDictionary<Type, IMauiActivityResult> _map = [];

	public IEnumerable<IMauiActivityResult> MauiActivityResults { get; } =
	[
		new MauiActivityResult<AndroidUri>(componentActivity, new ActivityResultContracts.PickVisualMedia()),	
	];

	public void Register(IMauiActivityResult mauiActivityResult)
	{
		if (_map.TryGetValue(mauiActivityResult.Contract.GetType(), out var existingActivityResult)
			&& existingActivityResult.Launcher is not null)
		{
			existingActivityResult.Launcher.Unregister();
		}

		mauiActivityResult.Register();
		_map.TryAdd(mauiActivityResult.Contract.GetType(), mauiActivityResult);
	}

	public static Task<JavaObject?> Launch<TContract>(JavaObject javaObject)
		where TContract : ActivityResultContract
	{
		if (_map.TryGetValue(typeof(TContract), out var mauiActivityResult))
		{
			return mauiActivityResult.Launch(javaObject);
		}

		return Task.FromResult(default(JavaObject));
	}

	public void RegisterAll()
	{
		foreach (var mauiActivityResult in MauiActivityResults)
		{
			Register(mauiActivityResult);
		}
	}
}


internal class MauiActivityResult<TResult>(ComponentActivity componentActivity, ActivityResultContract contract) : IMauiActivityResult
	where TResult : JavaObject
{
	static int _idCounter;
	static TaskCompletionSource<JavaObject?>? _tcs = null;
	private readonly ComponentActivity _componentActivity = componentActivity;

	public ActivityResultLauncher? Launcher { get; private set; }
	public int Id { get; } = Interlocked.Increment(ref _idCounter);
	public string IdString => $"{GetType().FullName}{Id}";
	public ActivityResultContract Contract { get; } = contract;
	public IActivityResultCallback Callback { get; } = new MauiActivityResultCallback<TResult>(result => _tcs?.SetResult(result));

	public void Register()
	{
		Launcher = _componentActivity.ActivityResultRegistry.Register(
			IdString,
			_componentActivity,
			Contract,
			Callback);
	}

	public Task<JavaObject?> Launch(JavaObject? param)
	{
		_tcs = new();

		if (Launcher is null)
		{
			_tcs.SetCanceled();
			return _tcs.Task;
		}

		try
		{
			Launcher.Launch(param);
		}
		catch (Exception ex)
		{
			_tcs.SetException(ex);
		}

		return _tcs.Task;
	}
}

internal interface IMauiActivityResult
{
	int Id { get; }
	ActivityResultContract Contract { get; }
	IActivityResultCallback Callback { get; }
	ActivityResultLauncher? Launcher { get; }
	void Register();
	Task<JavaObject?> Launch(JavaObject? param);
}
#nullable restore