using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private static QuestSystem instance;

    [SerializeField]
    private List<string> completedEvents = new List<string>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        OnQuestEvent("start");
    }

    public static bool IsEventCompleted(string id) => instance.completedEvents.Contains(id);

    public static void OnQuestEvent(string id)
    {
        instance.completedEvents.Add(id);

        switch (id)
        {
            case "start":
                PlayerSay("Какой отличный день! Вот бы сходить на рыбалку");
                QuestList.instance.AddQuest("go_fishing", "Пойти на рыбалку");
                Help.instance.SetHints("Отправляйся к озеру");
                break;
            case "interact_first_time_lake":
                PlayerSay("Так, а как рыбу ловить? Кажется я что-то забыл...");
                QuestList.instance.RemoveQuest("go_fishing");
                QuestList.instance.AddQuest("get_ready_for_fishing", "Найди удочку");
                break;
            case "interact_barn_lock":
                PlayerSay("Какой отличный день! Вот бы сходить на рыбалку");
                QuestList.instance.RemoveQuest("go_fishing");
                QuestList.instance.AddQuest("get_ready_for_fishing", "Найди удочку");
                break;
        }
    }

    private static void PlayerSay(string text) => DialogueBubble.instance.Setup(Player.instance.DialoguePosition, text);
}
