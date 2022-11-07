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
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        OnQuestEvent("start");

    }

    public static bool IsEventCompleted(string id) => instance.completedEvents.Contains(id);

    public static void OnQuestEvent(string id)
    {
        instance.completedEvents.Add(id);
        Map.Instance.UpdateItemsOnLocation();

        switch (id)
        {
            case "start":
                PlayerSay("Какой отличный день! вот бы сходить на рыбалку!", "Надо взять ведро");
                QuestList.instance.AddQuest("Bucket", "Найти ведро и пойти рыбачить", 3);
                Help.instance.SetHints("Ведра обычно хранятся в сарае", "Пазлы хранят секрет", "Хорошая рыба водится в озере");
                break;
            case "ded_dialogue_end_0":
                PlayerSay("Точно! дед ловил рыбу не лапами, а какой-то палкой");
                QuestList.instance.RemoveQuest("Bucket", 2);
                QuestList.instance.AddQuest("Stick", "Найти палку и пойти рыбачить", 3);
                Help.instance.SetHints("Найти хорошую палку");
                break;
            case "ded_dialogue_end_1":
                PlayerSay("Хм, и где же мне их найти?");
                QuestList.instance.RemoveQuest("Stick", 2);
                QuestList.instance.AddQuest("Tickle", "Найти снасти и пойти рыбачить", 3);
                Help.instance.SetHints("В журнале иногда пишут полезную информацию");
                break;
            case "ded_dialogue_end_2":
                PlayerSay("Вроде бы черви хорошо подойдут");
                QuestList.instance.RemoveQuest("Tickle", 2);
                QuestList.instance.AddQuest("Worms", "Найти наживку и пойти рыбачить", 3);
                Help.instance.SetHints("В пещере водятся хорошие черви", "Фонарик можно найти на площади", "Кто-то повесил карту верх ногами");
                break;
        }
    }

    private static void PlayerSay(string text) => DialogueBubble.instance.Setup(Player.instance.DialoguePosition, text, 4);

    private static void PlayerSay(params string[] texts) => instance.StartCoroutine(PlayerSayTextsImpl(texts));

    private static IEnumerator PlayerSayTextsImpl(string[] texts)
    {
        foreach (var text in texts)
        {
            PlayerSay(text);
            yield return new WaitForSeconds(4.5f);
        }
    }
}
