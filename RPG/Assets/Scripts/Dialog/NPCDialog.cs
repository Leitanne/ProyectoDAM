using System;
using UnityEngine;

public enum extraInteractionNPC
{
    Quest,
    Shop,
    Crafting
}

[CreateAssetMenu]
public class NPCDialog : ScriptableObject
{
    [Header("Info")]
    public string name;
    public Sprite icon;
    public bool hasExtraInteraction;
    public extraInteractionNPC typeOfExtraInteraction;

    [Header("Saludo")]
    [TextArea] public string greeting;

    [Header("Chat")]
    public TextDialog[] Conversation;

    [Header("Despedida")]
    [TextArea] public string farewell;
}

[Serializable]
public class TextDialog
{
    [TextArea] public string phrase;
}
