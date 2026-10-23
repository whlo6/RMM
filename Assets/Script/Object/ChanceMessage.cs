using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChanceMessage : MonoBehaviour
{
    public Text tips;

    public void OnClickOpen()
    {
        int n = Random.Range(0, 5);

        switch(n)
        {
            case 0:
                tips.text = "金币+300";
                PointMessage.Instance.gold += 300;
                break;
            case 1:
                tips.text = "生命最大值+10%";
                PointMessage.Instance.message[0] += (int)(PointMessage.Instance.message[0] * 0.1f);
                break;
            case 2:
                tips.text = "法力最大值+10%";
                PointMessage.Instance.message[1] += (int)(PointMessage.Instance.message[1] * 0.1f);
                break;
            case 3:
                tips.text = "消耗100金币\n恢复所有状态";
                PointMessage.Instance.gold -= 100;
                PointMessage.Instance.message[2] = PointMessage.Instance.message[0];
                PointMessage.Instance.message[3] = PointMessage.Instance.message[1];
                break;
            case 4:
                tips.text = "生命上限-25%\n恢复全部生命值";
                PointMessage.Instance.message[0] -= (int)(PointMessage.Instance.message[0] * 0.25f);
                PointMessage.Instance.message[2] = PointMessage.Instance.message[0];
                break;
        }
    }

    public void OnclickReturn()
    {
        SceneManager.LoadScene("Level");
    }
}
