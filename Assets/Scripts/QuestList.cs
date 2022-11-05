using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
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
        _image.enabled = true;
    }

    public void Close()
    {
        _image.enabled = false;
    }
}
