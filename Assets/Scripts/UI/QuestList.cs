using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : PopupElement
{
    public static QuestList instance;
    [SerializeField] private Text text;

    private List<string> questOrder = new List<string>();
    private Dictionary<string, string> activeQuests = new Dictionary<string, string>();

    private void Awake()
    {
        instance = this;
    }

    public void AddQuest(string name, string text, float showAfter)
    {
        questOrder.Add(name);
        activeQuests[name] = text;
        UpdateText();
        StartCoroutine(LaunchAfter(Show, showAfter));
        StartCoroutine(LaunchAfter(Close, showAfter + 3));
    }

    public void RemoveQuest(string name, float showAfter)
    {
        questOrder.Remove(name);
        activeQuests.Remove(name);
        UpdateText();
        StartCoroutine(LaunchAfter(Show, showAfter));
        StartCoroutine(LaunchAfter(Close, showAfter + 3));
    }

    public void Show()
    {
        ShowHide(true);
    }

    public void Close()
    {
        ShowHide(false);
    }

    private IEnumerator LaunchAfter(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    private void UpdateText()
    {
        text.text = "";
        foreach (var entry in questOrder)
        {
            text.text += " - " + activeQuests[entry] + "\n";
        }
    }
}
