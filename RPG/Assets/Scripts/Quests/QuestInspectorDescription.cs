using TMPro;
using UnityEngine;

public class QuestInspectorDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questReward;
    
    public override void QuestUIConfiguration(Quest quest)
    {
        base.QuestUIConfiguration(quest);
        questReward.text = $"-{quest.rewardGold} oro" + 
            $"\n-{quest.rewardExp} exp" +
            $"\n-{quest.rewardItem.Item.Name} x{quest.rewardItem.Amount}";
    }

    public void AcceptQuest()
    {
        if (QuestToComplete == null)
        {
            return;
        }

        QuestManager.Instance.AddQuest(QuestToComplete);
        gameObject.SetActive(false);
    }
}
