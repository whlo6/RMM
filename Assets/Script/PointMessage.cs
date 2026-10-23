using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//进度信息存放处
public class PointMessage : Singleton<PointMessage>
{
    public int[] message = new int[7];
    public int[] Rmessage = new int[7];

    //关卡进度信息
    public Dictionary<int, bool> levelsPass = new Dictionary<int, bool>();
    public int levelId = 0;//记录最新通关的关卡ID

    public int gold = 0;


    public void ResetEmpty()
    {
        this.message = new int[7];
        this.Rmessage = new int[7];
        this.levelsPass = new Dictionary<int, bool>();
        levelId = 0;
        gold = 0;
    }
}
