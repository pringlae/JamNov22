using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] private Minigame _minigamePrefab;
    [SerializeField] private string _succesfullyCompletedQuestKey;

    private bool _minigameCompleted;

    private void OnEnable()
    {
        _minigameCompleted = QuestSystem.IsEventCompleted(_succesfullyCompletedQuestKey);
    }

    public void Activate()
    {
        if (!_minigameCompleted)
            _minigamePrefab.Launch((success) => 
            {
                if (success)
                    QuestSystem.OnQuestEvent(_succesfullyCompletedQuestKey);
            });
        else
            DialogueBubble.instance.Setup(Player.instance.DialoguePosition, "Мне это больше не нужно.");
    }
}
