using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loding : MonoBehaviour
{
    public Slider proGress;
    public GameObject gameSatrt;
    float proGressNum;
    private void Start()
    {
        proGressNum = 0;
        VideoManager.Instance.SetPlaySpeed(0.5f);
        VideoManager.Instance.PlayVideo("Video/yk",true);
    }

    private void Update()
    {
        if(proGressNum < 100)
        {
            AddProGress();
        }
        else
        {
            proGress.gameObject.SetActive(false);
            gameSatrt.SetActive(true);
        }
    }

    void AddProGress()
    {
        proGressNum += 0.1f;
        proGress.value = proGressNum;
    }

    public void OnClick()
    {
        VideoManager.Instance.DestroySingleton();
        SceneManager.LoadScene("Lobby");
    }
}
