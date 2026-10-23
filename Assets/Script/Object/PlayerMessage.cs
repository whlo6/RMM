using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public int MHP;//最大生命
    public int MMP;//最大法力
    public int HP;//生命
    public int MP;//法力
    public int AP;//攻击
    public int SP;//速度
    public int WP;//防御

    public Dictionary<int, int> myCards = new Dictionary<int, int>();//记录卡牌Id
    public int cardsNum = 0;
    public bool isPlayer;//是否是玩家

    //数值显示UI组件
    public Slider uiHP;
    public Text UiHPnum;
    public Slider uiMP;
    public Text UiMPnum;
    public Text uiAP;
    public Text uiSP;
    public Text uiWP;

    public void Init(int[] mes)
    {
        this.MHP = mes[0];
        this.MMP = mes[1];
        this.HP = mes[2];
        this.MP = mes[3];
        this.AP = mes[4];
        this.SP = mes[5];
        this.WP = mes[6];
        this.SetMHPMMP();
        OnHpChange(0);
        OnMpChange(0);
        OnAddBuf(0, 0, 0);

        //测试用外挂
        //if (isPlayer)
        //{
        //    this.AP = 1000;
        //    this.SP = 100;
        //}

        int[] id = new int[5];
        for (int i = 0; i < id.Length; i++) id[i] = Random.Range(0, 5);
        AddCards(id);
    }

    //设置最大HP和MP
    public void SetMHPMMP()
    {
        uiHP.maxValue = this.MHP;
        uiMP.maxValue = this.MMP;
    }

    //状态改变
    public int OnHpChange(int hp)
    {
        this.HP += hp;
        if (this.HP > this.MHP) this.HP = this.MHP;
        
        this.uiHP.value = this.HP;
        this.UiHPnum.text = this.HP.ToString() + "/" + this.MHP.ToString();

        if (this.HP <= 0)
            if (isPlayer)
                return -1;
            else
                return 1;
        return 0;
    }

    public void OnMpChange(int mp)
    {
        this.MP += mp;
        if (this.MP > this.MMP) this.MP = this.MMP;

        this.uiMP.value = this.MP;
        this.UiMPnum.text = this.MP.ToString() + "/" + this.MMP.ToString();
    }

    public void OnAddBuf(int ap,int sp,int wp)//增减buf
    {
        this.AP += ap;
        this.SP += sp;
        this.WP += wp;

        if (this.SP < 5) this.SP = 5;
        if (this.SP > 95) this.SP = 95;
        if (this.WP > 90) this.WP = 90;

        this.uiAP.text = "AP:" + this.AP.ToString();
        this.uiSP.text = "SP:" + this.SP.ToString();
        this.uiWP.text = "WP:" + this.WP.ToString();
    }

    public void AddCards(int[] id)
    {
        for(int i=0;i<id.Length;i++)
        {
            this.myCards.Add(cardsNum, id[i]);
            cardsNum++;
        }

        if(isPlayer)
            CardsView.Instance.UpdateCardsArea();//更新显示区卡组
    }

    public void RemoveCard(int id)
    {
        this.myCards.Remove(id);
    }
}
