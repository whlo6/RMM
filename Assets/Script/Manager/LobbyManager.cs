using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : Mono<LobbyManager>//主界面管理
{
    public ChangePoint changePoint;

    public void OnClickEnterGame()
    {
        changePoint.SendMessage();
        SceneManager.LoadScene("Level");
        LobbyManager.Instance.DestroySingleton();
    }
}
