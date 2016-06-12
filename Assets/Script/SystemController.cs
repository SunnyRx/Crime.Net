using UnityEngine;
using System.Collections;

public class SystemController
{
    public struct member
    {
        public string name;
        public struct skills
        {
            public int leader;
            public int fight;
            public int dex;
            public int unlock;
            public int knowledge;
        }
        public skills skill;
        public member(string name, int leader, int fight, int dex, int unlock, int knowledge)
        {
            skill = new skills();
            this.name = name;
            this.skill.leader = leader;
            this.skill.fight = fight;
            this.skill.dex = dex;
            this.skill.unlock = unlock;
            this.skill.knowledge = knowledge;
        }
    }
    [SerializeField]private member mainPlayer;

    //单例模式
    private static SystemController instance = null;

    //获取实例
    public static SystemController GetInstance()
    {
        if (instance == null)
        {
            instance = new SystemController();
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

    public SystemController()
    {
        //初始化mainPlayer，输入测试数据
        mainPlayer.name = "无名冒险家";
        mainPlayer.skill.leader = 50;
        mainPlayer.skill.fight = 50;
        mainPlayer.skill.dex = 30;
        mainPlayer.skill.unlock = 80;
        mainPlayer.skill.knowledge = 70;
    }

    public member getMainPlayer()
    {
        return mainPlayer;
    }
}