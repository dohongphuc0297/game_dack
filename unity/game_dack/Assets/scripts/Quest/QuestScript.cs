using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{

    public Quest MyQuest { get; set; }

    private bool markedComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<Text>().color = Color.red;
        QuestLog.Instance.ShowDescription(MyQuest);
    }

    public void DeSeclect()
    {
        GetComponent<Text>().color = Color.black;
    }

    public void IsComplete()
    {
        if(MyQuest.IsComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponent<Text>().text += "(Hoan tat)";
        }
        else if(!MyQuest.IsComplete)
        {
            markedComplete = false;
            GetComponent<Text>().text = MyQuest.Title;
        }
    }
}
