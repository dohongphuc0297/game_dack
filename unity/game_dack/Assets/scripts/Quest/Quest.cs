using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private KillObjective[] killObjective;

    public QuestScript MyQuestScript { get; set; }

    public string Title { get => title; set => title = value; }

    public string Description { get => description; set => description = value; }

    public bool IsComplete
    {
        get
        {
            foreach(Objective obj in killObjective)
            {
                if (!obj.IsComplete)
                    return false;
            }
            return true;
        }
    }

    public KillObjective[] KillObjective
    {
        get
        {
            return killObjective;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int Amount { get => amount; set => amount = value; }

    public int CurrentAmount { get => currentAmount; set => currentAmount = value; }

    public string Type { get => type; set => type = value; }

    public bool IsComplete
    {
        get
        {
            return CurrentAmount >= Amount;
        }
    }
}

[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(BaseCharacterClass character)
    {
        if(Type == character.CharacterClassName)
        {
            CurrentAmount++;

            QuestLog.Instance.UpdateSelected();
            QuestLog.Instance.CheckCompletion();
        }
    }
}
