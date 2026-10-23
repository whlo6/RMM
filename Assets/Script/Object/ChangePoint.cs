using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePoint : MonoBehaviour
{
    public Slider[] sliderBar;
    public Text allPoint;
    int value;

    public Text message;
    int HP;//生命
    int MP;//法力
    int AP;//攻击
    int SP;//速度
    int WP;//防御

    private void Start()
    {
        this.HP = 300;
        this.MP = 100;
        this.AP = 10;
        this.SP = 5;
        this.WP = 0;
    }

    int SetAllPoint()
    {
        int num = 0;
        for(int i=0;i<sliderBar.Length;i++)
        {
            num = (int)(num + sliderBar[i].value);
        }

        if (num >= 30)
            allPoint.text = "剩余可用点数：0";
        else
            allPoint.text = "剩余可用点数：" + (30 - num).ToString();

        return num;
    }

    public void GetValue(Slider slider)
    {
        value = (int)slider.value;

        int num = SetAllPoint();
        if (num > 30)
        {
            this.value -= (num - 30);
            slider.value = this.value;
        }
    }
    public void OnValueChange(Text valueText)
    {
        valueText.text = this.value.ToString();
        SetMessage();
    }

    public void SetMessage()
    {
        //大量-----HP:100,MP:60,AP:10,SP:2,WP:2
        this.HP = (int)(300 + sliderBar[0].value * 100 + (sliderBar[2].value + sliderBar[3].value) * 50);
        this.MP = (int)(100 + sliderBar[3].value * 60);
        this.AP = (int)(10 + sliderBar[0].value * 10 + sliderBar[1].value * 5);
        this.SP = (int)(5 + sliderBar[1].value * 2 + sliderBar[2].value * 1);

        this.message.text = "当前角色数据" + "\nHP：" + this.HP + "\nMP：" + this.MP + "\nAP：" + this.AP + "\nSP：" + this.SP + "\nWP：" + this.WP;
    }

    public void SendMessage()
    {
        PointMessage.Instance.message[0] = this.HP;
        PointMessage.Instance.message[1] = this.MP;
        PointMessage.Instance.message[2] = this.HP;
        PointMessage.Instance.message[3] = this.MP;
        PointMessage.Instance.message[4] = this.AP;
        PointMessage.Instance.message[5] = this.SP;
        PointMessage.Instance.message[6] = this.WP;
    }
}
