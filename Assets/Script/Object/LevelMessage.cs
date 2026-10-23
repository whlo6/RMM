using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMessage : LevelType
{
    public E_LevelType type;
    public int[] message;
    int key;
    bool isPass;

    private void Start()
    {
        //获取基础信息
        string name = this.gameObject.name;
        if (name[1] != 'x')
            key = int.Parse(name[0].ToString() + name[1].ToString()) * 10 + int.Parse(name[3].ToString());
        else
            key = int.Parse(name[0].ToString()) * 10 + int.Parse(name[2].ToString());

        //验证通过信息
        if (!PointMessage.Instance.levelsPass.ContainsKey(key))
        {
            isPass = false;

            //记录所有关卡通过情况
            PointMessage.Instance.levelsPass.Add(key, isPass);
        }
        else
            isPass = PointMessage.Instance.levelsPass[key];
        if (isPass) this.Pass();

        //将战斗关卡ID及敌方数据存入怪物仓库
        if (!MonsterDepot.Instance.monster.ContainsKey(key) && type == E_LevelType.Fight)
            MonsterDepot.Instance.monster.Add(key, message);

    }

    public void OnClick()
    {
        //限制关卡选择，防止直接选最后一关
        if (this.key / 10 != PointMessage.Instance.levelId / 10 + 1) return;

        if(type == E_LevelType.Fight)
        {
            PointMessage.Instance.Rmessage = this.message;
            SceneManager.LoadScene("Fight");
        }
        if(type==E_LevelType.Rest)
        {
            SceneManager.LoadScene("Rest");
        }
        if (type == E_LevelType.Chance)
        {
            SceneManager.LoadScene("Chance");
        }

        //设置通过状态
        //for(int i=1; ;i++)
        //{
        //    int n = key / 10 * 10 + i;
        //    Debug.Log(key + "____" + n);
        //    if (!PointMessage.Instance.levelsPass.ContainsKey(n)) break;
        //    PointMessage.Instance.levelsPass[n] = true;
        //}
        PointMessage.Instance.levelsPass[key] = true;
        PointMessage.Instance.levelId = key;
        this.Pass();
    }
    
    public void Pass()//设置关卡为通过或无法选择状态
    {
        isPass = true;
        this.gameObject.SetActive(false);
    }
}
