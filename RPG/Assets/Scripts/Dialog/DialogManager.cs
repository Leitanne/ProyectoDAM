using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcChatTMP;

    public NPCInteraction NPCAvaliable { get; set; }

    private Queue<string> sequenceDialog;
    private bool animatedDialog;
    private bool showFarewell;

    private void Start()
    {
        sequenceDialog = new Queue<string>();
    }

    private void Update()
    {
        if(NPCAvaliable == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PanelConfiguration(NPCAvaliable.Dialog);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(showFarewell)
            {
                OpenCloseDialogPanel(false);
                showFarewell = false;
                return;
            }

            if(NPCAvaliable.Dialog.hasExtraInteraction)
            {
                UIManager.Instance.OpenInteractionPanel(NPCAvaliable.Dialog.typeOfExtraInteraction);
                OpenCloseDialogPanel(false);
                return;
            }

            if(animatedDialog)
            {
                ContinueDialog();
            }
        }
    }

    public void OpenCloseDialogPanel(bool state)
    {
        dialogPanel.SetActive(state);
    }

    private void PanelConfiguration(NPCDialog npcDialog)
    {
        OpenCloseDialogPanel(true);
        LoadDialogSequence(npcDialog);

        npcIcon.sprite = npcDialog.icon;
        npcNameTMP.text = $"{npcDialog.name}:";
        DisplayTextWithAnimation(npcDialog.greeting);

    }

    private void LoadDialogSequence(NPCDialog npcDialog)
    {
        if(npcDialog.Conversation == null ||npcDialog.Conversation.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialog.Conversation.Length; i++)
        {
            sequenceDialog.Enqueue(npcDialog.Conversation[i].phrase);
        }
    }

    private void ContinueDialog()
    {
        if(NPCAvaliable == null || showFarewell)
        {
            return;
        }

        if (sequenceDialog.Count == 0)
        {
            string farewell = NPCAvaliable.Dialog.farewell;
            DisplayTextWithAnimation(farewell);
            showFarewell = true;
            return;
        }

        string nextDialog = sequenceDialog.Dequeue();
        DisplayTextWithAnimation(nextDialog);
    }

    private IEnumerator AnimateText(string phrase)
    {
        animatedDialog = false;
        npcChatTMP.text = "";
        char[] letters = phrase.ToCharArray();

        for(int i=0; i < letters.Length; i++)
        {
            npcChatTMP.text += letters[i];
            yield return new WaitForSeconds(0.03f);
        }

        animatedDialog = true;
    }

    private void DisplayTextWithAnimation(string phrase)
    {
        StartCoroutine(AnimateText(phrase));
    }
}   
