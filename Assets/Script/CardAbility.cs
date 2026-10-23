using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbility : Singleton<CardAbility>
{
    private Dictionary<int, Action> ability = new Dictionary<int, Action>();
    private bool useSuccess;//卡牌使用是否成功
    private PlayerMessage left;
    private PlayerMessage right;

    public bool targetIsPlayer = false;
    private PlayerMessage user;//使用者
    private PlayerMessage target;//目标

    public void Init()
    {
        this.left = PlayerManager.Instance.left;
        this.right = PlayerManager.Instance.right;

        BindFunction(0, Fun0);
        BindFunction(1, Fun1);
        BindFunction(2, Fun2);
        BindFunction(3, Fun3);
        BindFunction(4, Fun4);
    }

    //id绑定方法并存仓
    private void BindFunction(int id, Action func)
    {
        if (ability.ContainsKey(id))
            ability[id] = func;
        else
            ability.Add(id, func);
    }

    // 卡牌效果
    public bool CardAssert(int id)
    {
        //绑定攻击者与被攻击者数据
        if(targetIsPlayer)
        {
            this.user = right;
            this.target = left;
        }
        else
        {
            this.user = left;
            this.target = right;
        }

        useSuccess = false;
        if (ability.TryGetValue(id,out Action targetFunc))
        {
            targetFunc.Invoke();
        }
        else
        {
            Debug.LogError($"无ID {id} 对应方法");
        }
        return useSuccess;
    }

    #region 卡牌实际效果方法群
    void Fun0()
    {
        if (user.MP < 50)
        {
            string str0;
            if (targetIsPlayer)
                str0 = "对手尝试使用" + CardDepot.Instance.cardsName[0] + "但似乎没有查克拉";
            else
                str0 = "玩家尝试使用" + CardDepot.Instance.cardsName[0] + "但似乎没有查克拉";
            MessageManager.Instance.CreatMessage(str0);
            return;
        }

        Debug.Log("使用卡牌垃圾话攻击");//AP:1.5  MP:50  SP:-5
        int harm = (int)(this.user.AP * 1.5f);
        if (this.target.WP != 0)
        {
            harm = (int)(harm * this.target.WP / 100);
        }

        string str;
        if (targetIsPlayer)
            str = "对手使用了" + CardDepot.Instance.cardsName[0] + "，造成了：" + harm + "点伤害";
        else
            str = "玩家使用了" + CardDepot.Instance.cardsName[0] + "，造成了：" + harm + "点伤害";
        MessageManager.Instance.CreatMessage(str);


        this.user.OnMpChange(-50);
        PlayerManager.Instance.win = this.target.OnHpChange(-harm);
        useSuccess = true;
    }

    void Fun1()
    {
        if (user.MP < 50)
        {
            string str0;
            if (targetIsPlayer)
                str0 = "对手尝试使用" + CardDepot.Instance.cardsName[1] + "但似乎没有查克拉";
            else
                str0 = "玩家尝试使用" + CardDepot.Instance.cardsName[1] + "但似乎没有查克拉";
            MessageManager.Instance.CreatMessage(str0);
            return;
        }

        int harm = 50;
        if (this.target.WP != 0)
        {
            harm = (int)(harm * this.target.WP / 100);
        }

        string str;
        if (targetIsPlayer)
            str = "对手使用了" + CardDepot.Instance.cardsName[1] + "，造成了：" + harm + "点伤害";
        else
            str = "玩家使用了" + CardDepot.Instance.cardsName[1] + "，造成了：" + harm + "点伤害";
        MessageManager.Instance.CreatMessage(str);

        this.user.OnMpChange(-50);
        PlayerManager.Instance.win = this.target.OnHpChange(-harm);
        useSuccess = true;
    }

    void Fun2()
    {
        if (user.MP < 50)
        {
            string str0;
            if (targetIsPlayer)
                str0 = "对手尝试使用" + CardDepot.Instance.cardsName[2] + "但似乎没有查克拉";
            else
                str0 = "玩家尝试使用" + CardDepot.Instance.cardsName[2] + "但似乎没有查克拉";
            MessageManager.Instance.CreatMessage(str0);
            return;
        }

        int harm = 50;
        if (this.target.WP != 0)
        {
            harm = (int)(harm * this.target.WP / 100);
        }

        string str;
        if (targetIsPlayer)
            str = "对手使用了" + CardDepot.Instance.cardsName[2] + "，造成了：" + harm + "点伤害";
        else
            str = "玩家使用了" + CardDepot.Instance.cardsName[2] + "，造成了：" + harm + "点伤害";
        MessageManager.Instance.CreatMessage(str);

        this.user.OnMpChange(-50);
        PlayerManager.Instance.win = this.target.OnHpChange(-harm);
        useSuccess = true;
    }

    void Fun3()
    {
        if (user.MP < 50)
        {
            string str0;
            if (targetIsPlayer)
                str0 = "对手尝试使用" + CardDepot.Instance.cardsName[3] + "但似乎没有查克拉";
            else
                str0 = "玩家尝试使用" + CardDepot.Instance.cardsName[3] + "但似乎没有查克拉";
            MessageManager.Instance.CreatMessage(str0);
            return;
        }

        int harm = 50;
        if (this.target.WP != 0)
        {
            harm = (int)(harm * this.target.WP / 100);
        }

        string str;
        if (targetIsPlayer)
            str = "对手使用了" + CardDepot.Instance.cardsName[3] + "，造成了：" + harm + "点伤害";
        else
            str = "玩家使用了" + CardDepot.Instance.cardsName[3] + "，造成了：" + harm + "点伤害";
        MessageManager.Instance.CreatMessage(str);

        this.user.OnMpChange(-50);
        PlayerManager.Instance.win = this.target.OnHpChange(-harm);
        useSuccess = true;
    }

    void Fun4()
    {
        if (user.MP < 50)
        {
            string str0;
            if (targetIsPlayer)
                str0 = "对手尝试使用" + CardDepot.Instance.cardsName[4] + "但似乎没有查克拉";
            else
                str0 = "玩家尝试使用" + CardDepot.Instance.cardsName[4] + "但似乎没有查克拉";
            MessageManager.Instance.CreatMessage(str0);
            return;
        }

        int harm = 50;
        if (this.target.WP != 0)
        {
            harm = (int)(harm * this.target.WP / 100);
        }

        string str;
        if (targetIsPlayer)
            str = "对手使用了" + CardDepot.Instance.cardsName[4] + "，造成了：" + harm + "点伤害";
        else
            str = "玩家使用了" + CardDepot.Instance.cardsName[4] + "，造成了：" + harm + "点伤害";
        MessageManager.Instance.CreatMessage(str);

        this.user.OnMpChange(-50);
        PlayerManager.Instance.win = this.target.OnHpChange(-harm);
        useSuccess = true;
    }
    #endregion

}
