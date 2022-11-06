using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ded : NPC
{
    [SerializeField] protected DialogueData[] _dialogues;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
    }

    public override void Activate()
    {
        if (_currentSpeech == -1)
        {

            _dialogueData = _dialogues[0];
            StartDialogue();
        }
        _currentSpeech++;
        if (_currentSpeech >= _dialogueData.speeches.Length)
        {
            StopDialogue();
            return;
        }
        var speech = _dialogueData.speeches[_currentSpeech];
        var pos = speech.isPlayer ? Player.instance.DialoguePosition : _bubblePosition.position;
        DialogueBubble.instance.Setup(pos, speech.text);
    }

    protected override void StartDialogue()
    {
        Player.instance.CanMove = false;
    }
}
