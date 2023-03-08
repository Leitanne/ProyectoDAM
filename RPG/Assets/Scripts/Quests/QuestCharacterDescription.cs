using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestCharacterDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questGoal;
    [SerializeField] private TextMeshProUGUI goldReward;
    [SerializeField] private TextMeshProUGUI expReward;

    [Header("Item")]
    [SerializeField] private Image rewardItemIcon;
    [SerializeField] private TextMeshProUGUI rewardItemAmount;

    
    private void Update()
    {
        if(QuestToComplete.questDoneCheck)
        {
            return;
        }
        
        questGoal.text = $"{QuestToComplete.currentAmount}/{QuestToComplete.amountObjective}";
    }
    
    public override void QuestUIConfiguration(Quest questToLoad)
    {
        base.QuestUIConfiguration(questToLoad);
        goldReward.text = questToLoad.rewardGold.ToString();
        expReward.text = questToLoad.rewardExp.ToString();
        questGoal.text = $"{questToLoad.currentAmount}/{questToLoad.amountObjective}";

        rewardItemIcon.sprite = questToLoad.rewardItem.Item.Icon;
        rewardItemAmount.text = questToLoad.rewardItem.Amount.ToString();
    }

    private void QuestCompletedResponse(Quest completedQuest)
    {
        if(completedQuest.id == QuestToComplete.id)
        {
            questGoal.text = $"{QuestToComplete.currentAmount}/{QuestToComplete.amountObjective}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(QuestToComplete.questDoneCheck)
        {
            gameObject.SetActive(false);
        }

        Quest.eventCompletedQuest += QuestCompletedResponse;
    }

    private void OnDisable()
    {
        Quest.eventCompletedQuest -= QuestCompletedResponse;
    }
}
