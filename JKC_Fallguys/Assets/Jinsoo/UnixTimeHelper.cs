using System;
using Cysharp.Threading.Tasks;

public static class UnixTimeHelper
{
    public static long GetFutureUnixTime(int delayTime)
    {
        long startUnixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() + delayTime;

        return startUnixTimestamp;
    }
    
    public static void ScheduleDelayedAction(long startUnixTimestamp, Action action)
    {
        ScheduleDelayUniTask(startUnixTimestamp, action).Forget();
    }

    private static async UniTaskVoid ScheduleDelayUniTask(long startUnixTimestamp, Action action)
    {
        int waitTimeSeconds = (int)(startUnixTimestamp - DateTimeOffset.Now.ToUnixTimeSeconds());
        if (waitTimeSeconds > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTimeSeconds));
        }
        
        action?.Invoke();
    }
}
