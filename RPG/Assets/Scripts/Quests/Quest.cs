using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> eventCompletedQuest;

    [Header("Info")]
    public string name;
    public string id;
    public int amountObjective;

    [Header("Description")]
    [TextArea] public string description;

    [Header("Rewards")]
    public int rewardGold;
    public float rewardExp;
    public QuestRewardItem rewardItem;

    [HideInInspector] public int currentAmount;
    [HideInInspector] public bool questDoneCheck;

    public void AddProgress(int amount)
    {
        currentAmount += amount;
        VerifyCompletedQuest();
    }

    private void VerifyCompletedQuest()
    {
        if (currentAmount >= amountObjective)
        {
            currentAmount = amountObjective;
            CompletedQuest();
        }
    }

    private void CompletedQuest()
    {
        if(questDoneCheck)
        {
            return;
        }

        questDoneCheck = true;
        eventCompletedQuest?.Invoke(this);
    }

    private void OnEnable()
    {
        questDoneCheck = false;
        currentAmount = 0;
    }
}

[Serializable]
public class QuestRewardItem
{
    public InventoryItem Item;
    public int Amount;
}