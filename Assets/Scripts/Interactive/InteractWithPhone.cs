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
                    StartCoroutine(StartDialogue(_bubblePosition, bubbleText));
            });
        else
        {
            StartCoroutine(StartDialogue(Player.instance.DialoguePosition, "Мне это больше не нужно."));
        }
    }

    private IEnumerator StartDialogue(Transform pos, string text)
    {
        yield return new WaitForSeconds(0.3f);
        _inDialogue = true;
        DialogueBubble.instance.Setup(pos, text);
        Player.instance.CanMove = false;
    }
}
