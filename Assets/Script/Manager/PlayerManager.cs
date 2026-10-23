using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Mono<PlayerManager>
{
    public GameObject stop;
    public TimeView timeView;
    public PlayerMessage left;
    public PlayerMessage right;
    public int win = 0;//是否胜利 -1失败，1胜利
    public bool isPlayCartoon = true;

    bool isPlayerTime = true;
    int leftTime;
    int rightTime;

    //敌方使用卡牌延迟执行，模拟真人思考
    void DelayUseCard()
    {
        EnemyManager.Instance.UseCard();
    }
    //胜利或失败动画
    void DelaySetPannel()
    {
        //记录当前状态
        PointMessage.Instance.message[2] = left.HP;
        PointMessage.Instance.message[3] = left.MP;
        PointMessage.Instance.message[4] = left.AP;
        PointMessage.Instance.message[5] = left.SP;
        PointMessage.Instance.message[6] = left.WP;

        GameOver.Instance.SetPannel();
    }
    //延迟恢复玩家操作
    void DelaySetActive()
    {
        this.stop.SetActive(false);
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        this.left.Init(PointMessage.Instance.message);
        this.right.Init(PointMessage.Instance.Rmessage);

        leftTime = 100 - left.SP;
        rightTime = 100 - right.SP;

        NextTimeFrom();
        VideoManager.Instance.SetPlaySpeed(1f);
    }

    private bool[] CreateTimeList()
    {
        bool[] timeList = new bool[5];
        int l = leftTime;
        int r = rightTime;
        bool plt = isPlayerTime;

        for (int i = 0; i < timeList.Length; i++)
        {
            isPlayerTime = NextTime(left.SP, right.SP);
            timeList[i] = isPlayerTime;
        }
        
        leftTime = l;
        rightTime = r;
        isPlayerTime = plt;

        return timeList;
    }

    public bool NextTime(int sp1,int sp2)
    {
        if(sp1<=0||sp2<=0)
        {
            Debug.Log("PlayerSPERROE!!!!!");
            return sp1 > sp2;
        }

        if (leftTime <= 0 && isPlayerTime) leftTime += 100;
        if (rightTime <= 0 && !isPlayerTime) rightTime += 100;
        while (leftTime > 0 && rightTime > 0)
        {
            leftTime -= sp1;
            rightTime -= sp2;
        }
        
        return leftTime <= 0;
    }

    public void NextTimeFrom()//计算下一个回合所属方
    {
        timeView.SetView(CreateTimeList());
        isPlayerTime = NextTime(left.SP, right.SP);
        if (isPlayerTime)
            if (!this.isPlayCartoon)//不播动画则直接恢复
                DelaySetActive();
            else if (this.stop.active)
                Invoke(nameof(DelaySetActive), 10f / VideoManager.Instance.playSpeed); //恢复玩家操作
            else;
        else
            this.stop.SetActive(true);//限制玩家操作

        int[] id = { Random.Range(0, 5) };
        //敌方操作
        if (!isPlayerTime)
        {
            Debug.Log("敌方正在思考");
            if (isPlayCartoon)
                Invoke(nameof(DelayUseCard), 13f / VideoManager.Instance.playSpeed);
            else
                Invoke(nameof(DelayUseCard), 3f);
            right.AddCards(id);
        }
        else
        {
            left.AddCards(id);
        }

    }

    public bool UseCard(int myCardId, int cardTypeId,bool isPlayer)//isPlayer释放目标是否为玩家(left)
    {
        if (win != 0) return false;//已经分出胜负，操作不受理
        //卡牌生效
        bool useSuccess;
        CardAbility.Instance.targetIsPlayer = !isPlayer;
        useSuccess = CardAbility.Instance.CardAssert(cardTypeId);
        if (!useSuccess) return useSuccess;

        if(isPlayCartoon)
            CardDepot.Instance.UseCard(cardTypeId);//卡牌动画
        //删除卡牌库中的卡
        if (isPlayer) left.RemoveCard(myCardId);
        else right.RemoveCard(myCardId);
        NextTimeFrom();//计算下一回合

        //判断是否胜利
        if (win != 0)
        {
            //根据情况调整弹出Game over界面的时间
            if (VideoManager.Instance.Speed.active)
                Invoke(nameof(DelaySetPannel), 5f);
            else if (VideoManager.Instance.Skip.active)
                DelaySetPannel();
            else
                Invoke(nameof(DelaySetPannel), 10f);
        }

        return useSuccess;
    }

    public void AddMP(int mp=100)
    {
        left.OnMpChange(mp);
        MessageManager.Instance.CreatMessage("玩家开始凝聚查克拉");
        if (PlayerManager.Instance.isPlayCartoon)
            VideoManager.Instance.PlayVideo("Video/njckl");
        NextTimeFrom();
    }

}
