using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAwaitAction : MonoBehaviour {
    public Button button;
    ActionEvent ae = null;
    // Use this for initialization
    async void Start () {
        ae = new ActionEvent();
        button.onClick.AddListener(OnButtonClick);
        await ae;
        Debug.LogWarning($"点击事件返回，继续向下执行");
        //等1次加一个回调通知，等多少次回调多少次
        await ae;
        Debug.LogWarning($"点击事件返回，继续向下执行");
    }
    void OnButtonClick()
    {
        Debug.LogWarning($"OnButtonClick");
        ae.Dispatch();//回调通知
    }
	// Update is called once per frame
	void Update () {
		
	}
}
