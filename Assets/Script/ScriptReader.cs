using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionCommandType
{
    NormalAction   //普通动作
}


public class ScriptReader {

    private List<ActionCommand> scriptList = null;

    private int currentCommandIndex = 0;

    //采取单例模式，实例
    private static ScriptReader instance = null;

    //获取实例
    public static ScriptReader GetInstance()
    {
        if (instance == null)
        {
            instance = new ScriptReader();
        }
        return instance;
    }

    //销毁实例
    public static void DestoryInstance()
    {
        if (instance != null)
        {
            instance = null;
        }
    }

    public ScriptReader()
    {
        scriptList = new List<ActionCommand>();
    }

    public void LoadJsonFile()
    {
        ACnormal testNormal = new ACnormal(0, "两头牛疯狂往你冲撞过来。");
        testNormal.AddSelect("往左闪避", 1);
        testNormal.AddSelect("往右闪避", 2);
        testNormal.AddSelect("往后闪避", 3);
        scriptList.Add(testNormal);

        ACnormal testNormal2 = new ACnormal(1, "两头牛突然分开往左右冲撞，你被撞到了");
        testNormal2.AddSelect("重头来过", 0);
        scriptList.Add(testNormal2);

        ACnormal testNormal3 = new ACnormal(2, "两头牛突然分开往左右冲撞，你被撞到了");
        testNormal3.AddSelect("重头来过", 0);
        scriptList.Add(testNormal3);

        ACnormal testNormal4 = new ACnormal(3, "两头牛突然分开往左右冲撞，你躲过一劫");
        testNormal4.AddSelect("重头来过", 0);
        scriptList.Add(testNormal4);

    }

    public void nextScript()
    {
        ActionCommand cmd = scriptList[currentCommandIndex++];
        switch (cmd.type)
        {
            case ActionCommandType.NormalAction:
                ((ACnormal)cmd).execute();
                break;
            default:
                Debug.Log("Unknow Command.");
                break;
        }
    }

    public void JumpTo(int index)
    {
        ActionCommand cmd = scriptList[index];
        switch (cmd.type)
        {
            case ActionCommandType.NormalAction:
                ((ACnormal)cmd).execute();
                break;
            default:
                Debug.Log("Unknow Command.");
                break;
        }
    }

    public void setWorkManger(WorkManager workManager)
    {
        
    }
}
