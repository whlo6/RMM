using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceManager : MonoBehaviour
{
    public Animator box;
    public Animator message;

    public void OnClickBox()
    {
        box.SetTrigger("open");
        message.SetTrigger("open");
    }
}
