﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//一个选项的信息
public struct OneSelect
{
    public string text;    //文本信息
    public int to;         //跳转信息
    public OneSelect(string text, int to)
    {
        this.text = text;
        this.to = to;
    }
}

public class WorkManager : MonoBehaviour {

    public Text mainText;
    private int textNumber = 0;

    /*
    public GameObject mainTextPanel;
    private int textCurrentY = -10;
    */

    public Button actionButton;   //執行動作列表按鈕
    public Button leaveButton;    //離開動作按鈕
    private bool isNeedShowAction = false;  //是否需要顯示執行動作列表按鈕
    private bool isNeedShowLeave = false;   //是否需要顯示離開動作按鈕

    public GameObject panel;    //储存选项的容器
    public GameObject theSelect;    //选项物件
    public GameObject Description;  //描述文字物件

    public OneSelect[] actionSelectList;
    public OneSelect[] leaveSelectList;
    OneSelect[] currentSelectList;
    List<GameObject> currentToggle;

    private bool isFirstStart = true;
    private int updateNumber = 0;

    public static WorkManager instance;

    void Awake()
    {
        mainText.text = "任务开始";

        isNeedShowAction = true;
        isNeedShowLeave = true;

        currentToggle = new List<GameObject>();
    }
        
    void Start()
    {
        instance = this;

        //初始化Action执行列表
        actionSelectList = new OneSelect[3];
        for (int i = 0; i < 3; i++)
        {
            actionSelectList[i].text = "行动 " + (i + 1);
            actionSelectList[i].to = i + 1;
        }
        //初始化Leave执行列表
        leaveSelectList = new OneSelect[1];
        for (int i = 0; i < 1; i++)
        {
            leaveSelectList[i].text = "你离开了现场";
            leaveSelectList[i].to = 0;
        }
            
        ScriptReader.GetInstance().LoadJsonFile();
        ScriptReader.GetInstance().JumpTo(0);

    }

	// Update is called once per frame
	void Update () {
        //Scroll View方法，因为Unity的BUG舍弃
        /*
        if (isFirstStart && updateNumber >= 2)
        {
            ScriptReader.GetInstance().JumpTo(0);
            isFirstStart = false;
        }
        else
        {
            if (isFirstStart)
                updateNumber++;
        }
        */
	}

    public void addText(string text)
    {
        mainText.text += "\n";
        mainText.text += text;
        //显示输出行数，超过则舍弃第一行
        if (textNumber < 18)
        {
            textNumber++;
        }
        else
        {
            int tmpInt = mainText.text.IndexOf('\n');
            mainText.text = mainText.text.Substring(tmpInt + 1);
        }
        //Scroll View方法，因为Unity的BUG舍弃
        /*
        GameObject tmpText = Instantiate(Description) as GameObject;
        tmpText.transform.SetParent(mainTextPanel.transform);
        tmpText.transform.localPosition = new Vector3(10,textCurrentY + 160,0f);
        tmpText.transform.localScale = new Vector3(1f,1f,1f);
        tmpText.GetComponent<Text>().text = text;

        textCurrentY -= (int)tmpText.GetComponent<RectTransform>().sizeDelta[1];
        if (System.Math.Abs(textCurrentY) >= mainTextPanel.GetComponent<RectTransform>().sizeDelta[1])
        {
            mainTextPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(mainTextPanel.GetComponent<RectTransform>().sizeDelta[0], tmpText.GetComponent<RectTransform>().sizeDelta[1] + System.Math.Abs(tmpText.transform.position.y));
        }
        */
    }

    public void nextText()
    {
        addText("测试");
        GetCurrentSelect();
    }

    public void GetCurrentSelect()
    {
        int currentSelect = -1;
        for (int i = 0; i < currentToggle.Count; i++)
        {
            if (currentToggle[i].GetComponent<Toggle>().isOn)
            {
                currentSelect = i;
                break;
            }
        }
        if (currentSelect != -1)
        {
            //执行对应选项的跳转
            string outputText = "执行" + currentSelectList[currentSelect].text + ",跳转至" + currentSelectList[currentSelect].to; 
            addText(outputText);
            ScriptReader.GetInstance().JumpTo(currentSelectList[currentSelect].to);
        }
    }

    //显示可执行的动作种类
    public void showSelect()
    {
        
        float currentX = -280;   //按鈕默認X
        if (isNeedShowAction)
        {
            actionButton.gameObject.SetActive(true);
            actionButton.transform.localPosition = new Vector3(currentX, -15, 0f);
            currentX += 50;
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }
        if (isNeedShowLeave)
        {
            leaveButton.gameObject.SetActive(true);
            leaveButton.transform.localPosition = new Vector3(currentX, -15, 0f);
            currentX += 50;
        }
        else
        {
            leaveButton.gameObject.SetActive(false);
        }
    }

    public void selectActionButton()
    {
        showSelectButtonList(actionSelectList);
    }

    public void selectLeaveButton()
    {
        showSelectButtonList(leaveSelectList);
    }

    //显示该种类可执行的行动列表
    public void showSelectButtonList(OneSelect[] selectList)
    {
        currentSelectList = selectList;   //标记当前的选项表

        //清空当前的Toggle
        for (int i = 0; i < currentToggle.Count; i++)
        {
            Destroy(currentToggle[i]);
            Debug.Log("释放了一个资源");
        }
        currentToggle.Clear();


        for (int i = 0; i < selectList.Length; i++)
        {
            GameObject tmpToggle = Instantiate(theSelect) as GameObject;
            tmpToggle.transform.SetParent(panel.transform);
            tmpToggle.transform.localPosition = new Vector3(-200,70 - i *30,0f);
            tmpToggle.transform.localScale = new Vector3(1f,1f,1f);
            tmpToggle.GetComponentInChildren<Text>().text = selectList[i].text;
            tmpToggle.GetComponent<Toggle>().group = panel.GetComponent<ToggleGroup>();
            currentToggle.Add(tmpToggle);
        }
    }


}
