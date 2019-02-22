using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestAwaitBreakPoint : MonoBehaviour {

	// Use this for initialization
	async void Start () {
        Debug.LogWarning($"Start");
        await Task.Delay(1000);
        Debug.LogWarning($"await 1s compelete");
        Debug.LogWarning($"BeginAwaitBreakPoint");
        AwaitBreakPoint abp = new AwaitBreakPoint();
        Debug.LogWarning($"await AwaitBreakPoint");
        ////如果await之前调用end,会打断await，永远返回不了
        abp.End();
        var a = await abp;
        Debug.LogWarning($"AwaitBreakPoint a: {a}");
        Debug.LogWarning($"AwaitBreakPoint compelete");
        Debug.LogWarning($"AwaitBreakPoint compelete1");
        Debug.LogWarning($"AwaitBreakPoint compelete2");
        Debug.LogWarning($"AwaitBreakPoint compelete3");
        await Task.Delay(1000);
        Debug.LogWarning($"AwaitBreakPoint compelete4");
        await Task.Delay(1000);
        Debug.LogWarning($"AwaitBreakPoint compelete5");


    }
    // Update is called once per frame
    void Update () {
		
	}
}
