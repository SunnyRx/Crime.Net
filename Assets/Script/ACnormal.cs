using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ACnormal : ActionCommand {

    private string text;

    private List<OneSelect> selectList;

	public ACnormal(int number, string text)
    {
		this.type = ActionCommandType.NormalAction;
        this.number = number;
        this.text = text;
        selectList = new List<OneSelect>();
    }

    public void AddSelect(string text, int number)
    {
        OneSelect tmpSelect = new OneSelect(text, number);
        selectList.Add(tmpSelect);
    }

    public new void execute()
    {
		WorkManager.instance.addText (text);

        OneSelect[] tmpSelectList = new OneSelect[selectList.Count];
		for (int i = 0; i < selectList.Count; i++)
		{
            tmpSelectList[i] = selectList[i];
		}
        WorkManager.instance.actionSelectList = tmpSelectList;
        WorkManager.instance.selectActionButton();
    }
}