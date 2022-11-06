using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private static QuestSystem instance;

    private List<string> completedEvents = new List<string>();

    void Awake()
    {
        instance = this;
    }

    public static bool IsEventCompleted(string id) => instance.completedEvents.Contains(id);

    public static void OnQuestEvent(string id)
    {
        instance.completedEvents.Add(id);
    }
}
