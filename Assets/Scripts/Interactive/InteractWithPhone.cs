using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithPhone : InteractWithMinigame
{
    [SerializeField] private Phone _phonePrefab;

    [SerializeField] private Transform _bubblePosition;
    private bool _inDialogue;

    public override void Activate()
    {
        if (_inDialogue)
        {
            _inDialogue = false;
            DialogueBubble.instance.Hide();
            Player.instance.CanMove = true;
            return;
        }
        if (!_minigameCompleted)
            _phonePrefab.Launch((success, bubbleText) =>
            {
                if (success)
                    QuestSystem.OnQuestEvent(_succesfullyCompletedQuestKey);

                if (bubbleText != "")
                    StartDialogue(_bubblePosition.position, bubbleText);
            });
        else
        {
            StartDialogue(Player.instance.DialoguePosition, "Мне это больше не нужно.");
        }
    }

    private void StartDialogue(Vector3 pos, string text)
    {
        _inDialogue = true;
        DialogueBubble.instance.Setup(pos, text);
        Player.instance.CanMove = false;
    }
}
