using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    private List<QuestScript> questScripts = new List<QuestScript>();

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questList;

    [SerializeField]
    private Text questDescription;

    private Quest selected;

    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            AcceptQuest(quests[i]);
        }
    }

    public void AcceptQuest(Quest quest)
    {
        foreach(KillObjective o in quest.KillObjective)
        {
            GameManager.Instance.killComfirmEvent += new KillConfirmed(o.UpdateKillCount);
        }

        GameObject go = Instantiate(questPrefab, questList);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        questScripts.Add(qs);

        go.GetComponent<Text>().text = quest.Title;
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void CheckCompletion()
    {
        foreach(QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSeclect();
            }

            selected = quest;

            string title = quest.Title;
            string description = quest.Description;

            string objectives = string.Empty;

            foreach (Objective obj in quest.KillObjective)
            {
                objectives += obj.Type + ":" + obj.CurrentAmount + "/" + obj.Amount + "\n";
            }

            questDescription.text = string.Format("<b>{0}</b>\n<size=12>{1}</size>\n\n<b>Nhiem vu</b>\n<size=12>{2}</size>", title, description, objectives);
        }
    }

}

