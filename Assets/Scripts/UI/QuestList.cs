using UnityEngine;
using UnityEngine.UI;

public class QuestList : PopupElement
{
    public static QuestList instance;
    [SerializeField] private Image _image;

    private void Awake()
    {
        instance = this;
    }

    public void SetQuest(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void Show()
    {
        ShowHide(true);
    }

    public void Close()
    {
        ShowHide(false);
    }
}
