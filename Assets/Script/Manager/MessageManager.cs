using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : Mono<MessageManager>
{
    public GameObject megPrefab;
    public GameObject content;

    GameObject[] message = new GameObject[30];
    int megNum = 0;

    public void CreatMessage(string str)
    {
        if (megNum > 20) ClearMessage();

        message[megNum] = Instantiate(megPrefab);
        message[megNum].transform.SetParent(content.transform);
        message[megNum].GetComponent<Message>().SetMessage(str);
        megNum++;
    }

    public void ClearMessage()
    {
        for (int i=0;i<megNum;i++)
        {
            Destroy(message[i]);
        }
        megNum = 0;
    }
}
