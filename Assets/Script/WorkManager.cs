using UnityEngine;
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
 
    public Button actionButton;   //執行動作列表按鈕
    public Button leaveButton;    //離開動作按鈕
    private bool isNeedShowAction = false;  //是否需要顯示執行動作列表按鈕
    private bool isNeedShowLeave = false;   //是否需要顯示離開動作按鈕

    public GameObject panel;    //储存选项的容器
    public GameObject theSelect;    //选项物件

    public OneSelect[] actionSelectList;
    public OneSelect[] leaveSelectList;
    OneSelect[] currentSelectList;
    List<GameObject> currentToggle;

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

        ScriptReader.GetInstance().setWorkManger(this);
        ScriptReader.GetInstance().LoadJsonFile();
        ScriptReader.GetInstance().JumpTo(0);
    }

	// Update is called once per frame
	void Update () {
    
	}

    public void addText(string text)
    {
        mainText.text += '\n';
        mainText.text += text;
    }

    public void nextText()
    {
        addText("测试");

        //showSelect();
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
