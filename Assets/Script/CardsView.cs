using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsView : Mono<CardsView>
{
    public PlayerMessage player;
    public GameObject cardsArea;
    public GameObject card;

    private void Awake()
    {
        CardDepot.Instance.Init();
        CardAbility.Instance.Init();
    }

    public void UpdateCardsArea()//更新卡牌区域
    {
        //清空
        for (int i = cardsArea.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = cardsArea.transform.GetChild(i);
            Object.Destroy(child.gameObject);
        }
        //重新生成
        foreach (var item in player.myCards)
        {
            GameObject obj = Instantiate(card);
            Card objCard = obj.GetComponent<Card>();
            objCard.cardTypeId = item.Value;//绑定卡牌类型ID
            objCard.myCardId = item.Key;//绑定牌库中的ID
            objCard.Init();
            obj.transform.SetParent(cardsArea.transform);
        }
    }
}
