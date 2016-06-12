using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public enum ActionCommandType
{
    NormalAction   //普通动作
}


public class ScriptReader {

    private List<ActionCommand> scriptList = null;  //脚本列表

    private int currentCommandIndex = 0;    //顺序执行时，当前脚本序号

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
        //打开Json文件
        var filepath = Path.Combine(Application.streamingAssetsPath, "process.json");
        FileInfo finfo = new FileInfo(filepath);
        string cmdStr = "";
        if (finfo.Exists)
        {
            StreamReader r = new StreamReader(filepath);
            cmdStr = r.ReadToEnd();
        }
        else
        {
            Debug.Log(filepath + " not found.");
        }
        //处理Json文件
        if (cmdStr.CompareTo("") != 0)
        {
            JSONNode jsonNode = JSONNode.Parse(cmdStr);
            JSONArray process = jsonNode["process"].AsArray;
            Debug.Log(jsonNode);
            foreach (JSONNode tmpNode in process)
            {
                Debug.Log("process start");
                Debug.Log("Process[" + tmpNode["number"].AsInt + "]");
                ACnormal tmpCmd = new ACnormal(tmpNode["number"].AsInt, tmpNode["true-text"], tmpNode["false-text"], tmpNode["requirement"], tmpNode["requirement-value"].AsInt);
                JSONArray selectList = tmpNode["select"].AsArray;
                foreach (JSONNode selectNode in selectList)
                {
                    tmpCmd.AddSelect(selectNode["text"], selectNode["to"].AsInt);
                }
                scriptList.Add(tmpCmd);
            }
        }

    }

    //顺序执行脚本
    public void nextScript()
    {
        ActionCommand cmd = scriptList[currentCommandIndex++];
        switch (cmd.type)
        {
            case ActionCommandType.NormalAction:
                ((ACnormal)cmd).Execute();
                break;
            default:
                Debug.Log("Unknow Command.");
                break;
        }
    }

    //执行对应序号的脚本
    public void JumpTo(int index)
    {
        ActionCommand cmd = scriptList[index];
        //根据单条脚本的类型执行对应的方法
        switch (cmd.type)
        {
            case ActionCommandType.NormalAction:
                ((ACnormal)cmd).Execute();
                break;
            default:
                Debug.Log("Unknow Command.");
                break;
        }
    }
}
