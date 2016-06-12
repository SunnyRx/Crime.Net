using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ACnormal : ActionCommand {

    private string trueText;
    private string falseText;

    private string requirement = "";
    private int requirementValue = 0;

    private List<OneSelect> selectList;

    public ACnormal(int number, string text, string text2 = "", string rText = "", int requirementValue = 0)
    {
		this.type = ActionCommandType.NormalAction;
        this.number = number;
        this.trueText = text;
        this.falseText = text2;
        this.requirement = rText;
        this.requirementValue = requirementValue;
        selectList = new List<OneSelect>();
    }

    public void AddSelect(string text, int number)
    {
        OneSelect tmpSelect = new OneSelect(text, number);
        selectList.Add(tmpSelect);
    }

    public new void Execute()
    {
        //第一阶段，判断是否有条件，如果有则直接略过此步到第二阶段
        if (requirement.CompareTo("null") != 0)
        {
            //存在条件
            //WorkManager.instance.addText("存在条件，条件为" + requirement +"，条件系数为0");
            Debug.Log("存在条件，条件为" + requirement +"，条件系数为0");
            //判断是否成功通过判定
            if (Judge() == false)
            {
                //如果判定失败，显示失败描述然后返回
                WorkManager.instance.addText(falseText);
                return;
            }
        }
        //第二阶段，显示文字，如果有判定条件并且判定成功将会跳到这一步
		WorkManager.instance.addText (trueText);
        //第三阶段，刷新屏幕的选项
        OneSelect[] tmpSelectList = new OneSelect[selectList.Count];
		for (int i = 0; i < selectList.Count; i++)
		{
            tmpSelectList[i] = selectList[i];
		}
        WorkManager.instance.actionSelectList = tmpSelectList;
        WorkManager.instance.selectActionButton();
    }

    public bool Judge()
    {
        //根据requirement的类型进行判断
        switch (requirement)
        {
            case "leader":
                //WorkManager.instance.addText("结合你相关技能成功率为" + (SystemController.GetInstance().getMainPlayer().skill.leader - requirementValue) + "%");
                return rpgJudge(SystemController.GetInstance().getMainPlayer().skill.leader - requirementValue);
                break;
            case "fight":
                return rpgJudge(SystemController.GetInstance().getMainPlayer().skill.fight - requirementValue);
                break;
            case "dex":
                return rpgJudge(SystemController.GetInstance().getMainPlayer().skill.dex - requirementValue);
                break;
            case "unlock":
                return rpgJudge(SystemController.GetInstance().getMainPlayer().skill.unlock - requirementValue);
                break;
            case "knowledge":
                return rpgJudge(SystemController.GetInstance().getMainPlayer().skill.knowledge - requirementValue);
                break;
            default:
                return false;
                break;
        }
    }
        
    public bool rpgJudge(int value)
    {
        //使用传统的RPG点数判断方法
        int randomValue = Random.Range(0, 100);
        if (randomValue <= value)
            return true;
        else
            return false;
    }
}