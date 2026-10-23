using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestManager : MonoBehaviour//ÐÝÏ¢³¡¾°
{
    public Slider uiHP;
    public Slider uiMP;
    public Text uiHPTips;
    public Text uiMPTips;

    private void Start()
    {
        SetUi();
    }

    public void OnRest()
    {
        PointMessage.Instance.message[2] += (int)(PointMessage.Instance.message[0] * 0.3);//»Ö¸´30%HP
        PointMessage.Instance.message[3] = PointMessage.Instance.message[1];//»Ö¸´100%MPs
        SetUi();
        Invoke(nameof(DelayReturn), 3f);
    }

    public void SetUi()
    {
        uiHP.maxValue = PointMessage.Instance.message[0];
        uiHP.value = PointMessage.Instance.message[2];
        uiHPTips.text = uiHP.value.ToString() + "/" + uiHP.maxValue.ToString();

        uiMP.maxValue = PointMessage.Instance.message[1];
        uiMP.value = PointMessage.Instance.message[3];
        uiMPTips.text = uiMP.value.ToString() + "/" + uiMP.maxValue.ToString();
    }

    void DelayReturn()
    {
        SceneManager.LoadScene("Level");
    }
}
