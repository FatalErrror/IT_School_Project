using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void DelayFunc();
    static List<Call> Calls;
    static bool isNewCall;
    struct Call {
        public float Duration;
        public DelayFunc delayFunc;
        public Call(float Duration, DelayFunc delayFunc)
        {
            this.Duration = Duration;
            this.delayFunc = delayFunc;
        }
    }
    public static void DelayCall(float Duration, DelayFunc delayFunc)
    {
        Calls.Add(new Call(Duration, delayFunc));
        isNewCall = true;
    }

    public void FixedUpdate()
    {
        if (isNewCall)
        {
            while (Calls.Count > 0) 
            {
                StartCoroutine("timer", Calls[0]);
                Calls.RemoveAt(0);
            }
        }
    }

    private IEnumerable timer(Call call)
    {
        yield return new WaitForSecondsRealtime(call.Duration);
        try
        {
            call.delayFunc();
        }
        catch { }
    }

}
