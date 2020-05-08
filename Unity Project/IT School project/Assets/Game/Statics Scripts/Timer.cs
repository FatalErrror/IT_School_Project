using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void DelayFunc();
    delegate Coroutine Call(float Duration, DelayFunc delayFunc, bool UnScaledTime);
    static event Call NewCall;

    public static Coroutine DelayCall(float Duration, DelayFunc delayFunc)
    {
        return NewCall(Duration, delayFunc, true);
    }

    public static Coroutine DelayCall(float Duration, DelayFunc delayFunc, bool UnScaledTime)
    {
        return NewCall(Duration, delayFunc, UnScaledTime);
    }

    void Start()
    {
        NewCall += call;
    }

    public Coroutine call(float Duration, DelayFunc delayFunc, bool UnScaledTime)
    {
        CallData = (Duration, delayFunc, UnScaledTime);
        return StartCoroutine(timer());
    }


    (float, DelayFunc, bool) CallData;

    public IEnumerator timer()
    {
        if (CallData.Item3)
            yield return new WaitForSecondsRealtime(CallData.Item1);
        else
            yield return new WaitForSeconds(CallData.Item1);
        try
        {
            CallData.Item2();
        }
        catch { }
    }

}
