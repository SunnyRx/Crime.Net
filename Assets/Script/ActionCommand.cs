using UnityEngine;
using System.Collections;

public abstract class ActionCommand : MonoBehaviour {

    public ActionCommandType type;  //动作类型

	private WorkManager workManager;

    public int number;  //该动作的序号

    public void execute()
    {

    }

}
