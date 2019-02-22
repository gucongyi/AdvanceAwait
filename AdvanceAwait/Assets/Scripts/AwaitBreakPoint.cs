using System;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// INotifyCompletion 在.NET 4.x才有，所有unity设置过来才可以
/// </summary>
public class AwaitBreakPoint : INotifyCompletion
{
    private bool isRun = true;
    private Action continuation;
    public AwaitBreakPoint()
    {
        Debug.LogWarning($"构造函数");
    }
    //必须有这个
    public bool IsCompleted
    {
        get
        {
            Debug.LogWarning($"IsCompleted");
            return isRun;
        }
    }
    //用来打断Await
    public void End()
    {
        isRun = false;
        Debug.LogWarning($"End");
    }
    //必须有这个，用来执行完成后到哪里，否在阻塞
    public void OnCompleted(Action continuation)
    {
        //IsCompleted 为true时，不会走到这里，false时才会走到这里，调用了continuation false才会await回来，执行后边的代码
        this.continuation = continuation;
        if (isRun == false)
        {
            Debug.LogWarning($"打断了，去做了另外一件事");
            continuation();
            //continuation();//continuation会完成后边的await
            //continuation();//continuation会完成后边的await
            Debug.LogWarning($"继续向下执行");
        }
    }
    //必须有这个，相当于Task<T>的返回值
    public int GetResult()
    {
        Debug.LogWarning($"GetResult");
        return 100;
    }
    //必须有这个
    public AwaitBreakPoint GetAwaiter()
    {
        Debug.LogWarning($"GetAwaiter");
        return this;
    }
}