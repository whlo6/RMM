using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public Text meg;

    public void SetMessage(string str)
    {
        meg.text = str;
        if(CardAbility.Instance.targetIsPlayer)
            meg.color = Color.red;
        else
            meg.color = Color.blue;
    }
}
