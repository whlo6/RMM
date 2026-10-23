using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    public GameObject[] playerTime;

    public void SetView(bool[] time)
    {
        for(int i=0;i<time.Length;i++)
        {
            playerTime[i].SetActive(!time[i]);
        }
    }
}
