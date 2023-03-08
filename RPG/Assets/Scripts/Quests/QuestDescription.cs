using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;

    public Quest QuestToComplete { get; set; }

    public virtual void QuestUIConfiguration(Quest quest)
    {
        QuestToComplete = quest;
        questName.text = quest.name;
        questDescription.text = quest.description;
    }
}
