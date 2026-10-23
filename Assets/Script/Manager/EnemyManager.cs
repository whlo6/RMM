using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Mono<EnemyManager>//敌人管理
{
    public PlayerMessage enemy;
    public RectTransform videoRect;

    public void UseCard()
    {
        if(PlayerManager.Instance.isPlayCartoon)//如果播放动画则镜像播放器
            videoRect.rotation = Quaternion.Euler(0, 180, 0);
        bool useSuccess = false;
        int i = 0;
        while(!useSuccess)
        {
            int myCardId = Random.Range(0, enemy.cardsNum);
            if (enemy.myCards.ContainsKey(myCardId))
                useSuccess = PlayerManager.Instance.UseCard(myCardId, enemy.myCards[myCardId], false);
            if (useSuccess && PlayerManager.Instance.isPlayCartoon)//如果找到可使用的卡则播放完后恢复播放器
            {
                Invoke(nameof(DelayRect), 10f / VideoManager.Instance.playSpeed);
            }
            if ((enemy.MP <= 50 || i > 20) && !useSuccess)//蓝不够或者找了20次都无可用牌，使用回蓝
            {
                Debug.Log("敌方使用回蓝");
                videoRect.rotation = Quaternion.Euler(0, 0, 0);//使用回蓝则直接恢复播放器
                MessageManager.Instance.CreatMessage("对手开始凝聚查克拉");
                if (PlayerManager.Instance.isPlayCartoon)
                    VideoManager.Instance.PlayVideo("Video/njckl");
                enemy.OnMpChange(100);
                PlayerManager.Instance.NextTimeFrom();
                useSuccess = true;
            }
            i++;
        }
    }

    void DelayRect()
    {
        videoRect.rotation = Quaternion.Euler(0, 0, 0);
    }
}
