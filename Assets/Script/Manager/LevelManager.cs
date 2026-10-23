using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text T_gold;

    private void Start()
    {
        T_gold.text = PointMessage.Instance.gold.ToString();
    }
}
