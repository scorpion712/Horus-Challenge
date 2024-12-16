namespace Horus_Challenge.Helpers.Utils;

public static class MainThreadHelper
{
    public static async Task BeginInvokeOnMainThread(Action action)
    {
        if (IsTestThread)
        {
            await Task.Run(action);
            return;
        }
        MainThread.BeginInvokeOnMainThread(action);
    }

    public static bool IsTestThread => DeviceInfo.Platform == DevicePlatform.Unknown;
}
