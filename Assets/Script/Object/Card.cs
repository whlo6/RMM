using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardTypeId;
    public int myCardId;
    public Text name;

    public void Init()
    {
        this.name.text = CardDepot.Instance.cardsName[cardTypeId];
    }

    public void OnClick()
    {
        if (!PlayerManager.Instance.UseCard(myCardId, cardTypeId, true)) return;

        Destroy(this.gameObject);
    }
}
