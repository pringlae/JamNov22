using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ded : NPC
{
    public static Ded instance;
    [SerializeField] protected DialogueData[] _dialogues;

    int _currentDialogueIndex;


    private void Awake()
    {
        instance = this;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            if (QuestSystem.IsEventCompleted("item_taken_worms"))
            {
                return;
            }
            if (Inventory.Instance.CurrentItem != null && Inventory.Instance.CurrentItem.Id == "tackle")
            {
                _currentDialogueIndex = 2;
                _dialogueData = _dialogues[2];
                Player.instance.CanInteract(instance);
                Activate();
            }
            if (Inventory.Instance.CurrentItem != null && Inventory.Instance.CurrentItem.Id == "stick3")
            {
                _currentDialogueIndex = 1;
                _dialogueData = _dialogues[1];
                Player.instance.CanInteract(instance);
                Activate();
            }
            if (Inventory.Instance.CurrentItem != null && Inventory.Instance.CurrentItem.Id == "bucket")
            {
                _currentDialogueIndex = 0;
                _dialogueData = _dialogues[0];
                Player.instance.CanInteract(instance);
                Activate();
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
    }

    public static void StartIteraction()
    {
        Player.instance.CanInteract(instance);

    }

    public override void Activate()
    {
        if (_currentSpeech == -1)
        {


            StartDialogue();
        }
        _currentSpeech++;
        if (_currentSpeech >= _dialogueData.speeches.Length)
        {
            StopDialogue();
            QuestSystem.OnQuestEvent("ded_dialogue_end_" + _currentDialogueIndex);
            return;
        }
        var speech = _dialogueData.speeches[_currentSpeech];
        var pos = speech.isPlayer ? Player.instance.DialoguePosition : _bubblePosition;
        DialogueBubble.instance.Setup(pos, speech.text);
    }

    protected override void StartDialogue()
    {
        Player.instance.CanMove = false;
    }
}
