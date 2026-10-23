using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : Mono<GameOver>
{
    public GameObject pannel;
    public GameObject reward;
    public Text title;

    bool isWin = false;

    public void SetPannel()
    {
        this.pannel.SetActive(true);

        if (PlayerManager.Instance.win == 1)
        {
            title.text = "胜利";
            isWin = true;

            int n = 100;
            reward.GetComponent<Text>().text = "金币+" + n;
            reward.SetActive(true);
            PointMessage.Instance.gold += n;//增加金币
            PlayerManager.Instance.right.gameObject.SetActive(false);
        }
        else
        {
            title.text = "失败";
            isWin = false;
            PlayerManager.Instance.left.gameObject.SetActive(false);
        }
    }

    public void OnClickReturn()
    {
        if(isWin)
        {
            VideoManager.Instance.DestroySingleton();
            SceneManager.LoadScene("Level");
        }
        else
        {
            PointMessage.Instance.ResetEmpty();
            SceneManager.LoadScene("Lobby");
        }
    }
}
