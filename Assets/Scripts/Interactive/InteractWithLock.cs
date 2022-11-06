using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithLock : InteractWithMinigame
{
    [SerializeField] private GameObject _door;

    void Start()
    {
        if (QuestSystem.IsEventCompleted("lock"))
        {
            OpenDoor();
        }
    }

    public override void Activate()
    {

        if (!_minigameCompleted)
            _minigamePrefab.Launch((success) =>
            {
                if (success)
                {
                    QuestSystem.OnQuestEvent(_succesfullyCompletedQuestKey);
                    OpenDoor();
                }
            });
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        _door.SetActive(true);
        Destroy(gameObject);
    }
}
