using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDepot : Singleton<CardDepot>
{
    public Dictionary<int, string> path = new Dictionary<int, string>();
    public Dictionary<int, string> cardsName = new Dictionary<int, string>();

    public void Init()
    {
        #region 添加卡牌路径和名字

        path.Add(path.Count, "Video/fhgj");//0
        cardsName.Add(cardsName.Count, "垃圾话攻击");

        path.Add(path.Count, "Video/yfs");//1
        cardsName.Add(cardsName.Count, "影分身");

        path.Add(path.Count, "Video/lxw");//2
        cardsName.Add(cardsName.Count, "螺旋丸");

        path.Add(path.Count, "Video/qn");//3
        cardsName.Add(cardsName.Count, "千鸟");

        path.Add(path.Count, "Video/sys");//4
        cardsName.Add(cardsName.Count, "色诱术");

        path.Add(path.Count, "Video/yk");//5
        cardsName.Add(cardsName.Count, "夜凯");

        #endregion
    }

    public void UseCard(int id)
    {
        VideoManager.Instance.PlayVideo(path[id]);
    }
}
