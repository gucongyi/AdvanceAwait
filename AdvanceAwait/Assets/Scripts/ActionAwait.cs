using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class ActionEvent
{
    private Action action;
    //全局的static，用来管理所有的ActionEvent
    private static readonly List<ActionEvent> mAll = new List<ActionEvent>();

    public ActionEvent()
    {
        mAll.Add(this);
    }
    //await ActionEvent 和ActionAwait关联
    public ActionAwait GetAwaiter()
    {
        return new ActionAwait(this);
    }
    public void Dispatch()
    {
        //调用Action,一般在回调里调用，比如等点击完成
        action?.Invoke();
    }
    public void Clear()
    {
        action = null;
    }
    public static void ClearAll()
    {
        mAll.ForEach(x => x.Clear());
        mAll.Clear();
    }
    public void AddListener(Action listener)
    {
        action += listener;
    }
    public void RemoveListener(Action listener)
    {
        action -= listener;
    }
}
public class ActionAwait : INotifyCompletion
{
    private ActionEvent actionEvent;
    private bool isActioned = false;
    private Action continuation;

    public ActionAwait(ActionEvent actionEvent)
    {
        this.actionEvent = actionEvent;
        if (actionEvent == null)
        {
            return;
        }
    }
    public bool IsCompleted
    {
        get
        {
            return isActioned;
        }
    }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
        if (actionEvent == null)
        {
            isActioned = true;
            continuation();
            return;
        }
        //给Action注册事件
        actionEvent.AddListener(OnAction);

    }
    public void GetResult()
    {

    }
    //Action事件回调
    private void OnAction()
    {
        //Debug.Log("OnAction");
        isActioned = true;
        actionEvent.RemoveListener(OnAction);
        //当前Await返回，继续向下执行
        continuation();
    }
}