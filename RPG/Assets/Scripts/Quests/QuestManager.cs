using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Character")]
    [SerializeField] Character character;

    [Header("Quests")]
    [SerializeField] private Quest[] avaliableQuest;

    [Header("Inspector quests")]
    [SerializeField] private QuestInspectorDescription inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContainer;

    [Header("Character quests")]
    [SerializeField] private QuestCharacterDescription characterQuestPrefab;
    [SerializeField] private Transform characterQuestContainer;

    [Header("Panel Quest Completed")]
    [SerializeField] private GameObject questCompletedPanel;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questGoldReward;
    [SerializeField] private TextMeshProUGUI questExpReward;
    [SerializeField] private TextMeshProUGUI questItemRewardAmount;
    [SerializeField] private Image questRewardItemIcon;

    public Quest QuestToReclaim { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        LoadQuestOnInspector();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            AddProgress("Kill10", 1);
            AddProgress("Kill25", 1);
            AddProgress("Kill50", 1);
        }
    }

    public void LoadQuestOnInspector()
    {
        for (int i = 0; i < avaliableQuest.Length; i++)
        {
            QuestInspectorDescription newQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContainer);
            newQuest.QuestUIConfiguration(avaliableQuest[i]);
        }
    }

    private void AddQuestToComplete(Quest questToComplete)
    {
        QuestCharacterDescription newQuest = Instantiate(characterQuestPrefab, characterQuestContainer);
        newQuest.QuestUIConfiguration(questToComplete);
    }

    public void AddQuest(Quest questToComplete)
    {
        AddQuestToComplete(questToComplete);
    }

    public void ReclaimReward()
    {
        if(QuestToReclaim == null)
        {
            return;
        }

        CoinManager.Instance.AddCoin(QuestToReclaim.rewardGold);
        character.characterExperience.AddExperience(QuestToReclaim.rewardExp);
        Inventory.Instance.AddItem(QuestToReclaim.rewardItem.Item, QuestToReclaim.rewardItem.Amount);
        questCompletedPanel.SetActive(false);
        QuestToReclaim = null;
    }

    public void AddProgress(string questID, int amount)
    {
        Quest questToUpdate = CheckQuestExistance(questID);
        questToUpdate.AddProgress(amount);
    }

    private Quest CheckQuestExistance(string questID)
    {
        for (int i = 0; i < avaliableQuest.Length; i++)
        {
            if (avaliableQuest[i].id == questID)
            {
                return avaliableQuest[i];
            }
        }

        return null;
    }

    private void ShowCompletedQuest(Quest completedQuest)
    {
        questCompletedPanel.SetActive(true);
        questName.text = completedQuest.name;
        questGoldReward.text = completedQuest.rewardGold.ToString();
        questExpReward.text = completedQuest.rewardExp.ToString();
        questItemRewardAmount.text = completedQuest.rewardItem.Amount.ToString();
        questRewardItemIcon.sprite = completedQuest.rewardItem.Item.Icon;
    }

    private void QuestCompletedResponse(Quest completedQuest)
    {
        QuestToReclaim = CheckQuestExistance(completedQuest.id);
        
        if (QuestToReclaim != null)
        {
            ShowCompletedQuest(QuestToReclaim);
        }
    }

    private void OnEnable()
    {
        Quest.eventCompletedQuest += QuestCompletedResponse;
    }

    private void OnDisable()
    {
        Quest.eventCompletedQuest -= QuestCompletedResponse;
    }
}
