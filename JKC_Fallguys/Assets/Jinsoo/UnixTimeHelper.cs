using System;
using Cysharp.Threading.Tasks;

public static class UnixTimeHelper
{
    private static long _serverTimeOffset = 0;
    
    public static void SyncServerTime(long serverUnixTime)
    {
        long localTime = GetCurrentUnixTime();
        _serverTimeOffset = serverUnixTime - localTime;
    }
    
    /// <summary>
    /// 지정한 지연 시간 후의 유닉스 시간(초 단위)을 반환하는 메서드입니다.
    /// 예를 들어, 현재 시간으로부터 5초 후의 유닉스 시간을 얻으려면 GetFutureUnixTime(5)를 호출하면 됩니다.  
    /// </summary>
    /// <param name="delayTime">지연 시간입니다.</param>
    public static long GetFutureUnixTime(int delayTime)
    {
        long startUnixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() + delayTime;

        return startUnixTimestamp;
    }
    
    public static long GetCurrentUnixTime()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
    
    /// <summary>
    /// 지정한 유닉스 시간이 될 때까지 대기한 후 액션을 실행하는 메서드입니다.
    /// 예를 들어, 현재 시간으로부터 5초 후에 특정 액션을 수행하려면
    /// 먼저 GetFutureUnixTime(5)를 호출하여 5초 후의 유닉스 시간을 얻은 후,
    /// 그 시간과 액션을 ScheduleDelayedAction에 전달하면 됩니다.
    /// </summary>
    /// <param name="startUnixTimestamp">UnixTimeHelper.GetFutureUnixTime로 반환된 값을 인수로 넣어줍니다.</param>
    /// <param name="action">실행할 동작을 정의합니다.</param>
    public static void ScheduleDelayedAction(long startUnixTimestamp, Action action)
    {
        ScheduleDelayUniTask(startUnixTimestamp, action).Forget();
    }

    // 내부적으로 ScheduleDelayedAction 메서드에서 사용되는 비동기 메서드입니다.
    // 이 메서드는 유닉스 시간이 될 때까지 대기하고, 액션을 실행합니다.
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
