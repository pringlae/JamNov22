using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerDownHandler
{
    public static Inventory Instance;

    [SerializeField] private Image _background, _highlight, _icon;

    public Item CurrentItem = null;
    

    void Awake()
    {
        Instance = this;
        SetHighlighted(false);
        UpdateIcon();
    }

    public bool MouseOnInventory =>
        RectTransformUtility.RectangleContainsScreenPoint(_background.rectTransform, Input.mousePosition, Camera.main);

    public void SetHighlighted(bool value)
    {
        _highlight.gameObject.SetActive(value);
    }

    public void PutItem(Item item)
    {
        if (CurrentItem != null)
        {
            CurrentItem.transform.parent = Map.Instance.CurrentLocation.transform;
            CurrentItem.transform.position = Player.instance.transform.position + Vector3.up;
            CurrentItem.gameObject.SetActive(true);
        }
        CurrentItem = item;
        CurrentItem.gameObject.SetActive(false);
        CurrentItem.transform.parent = null;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (CurrentItem != null)
        {
            _icon.sprite = CurrentItem.Sprite;
            _icon.color = CurrentItem.SpriteColor;
            _icon.gameObject.SetActive(true);
        }
        else
        {
            _icon.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CurrentItem) return;

        CurrentItem.transform.parent = Map.Instance.CurrentLocation.transform;
        CurrentItem.gameObject.SetActive(true);
        CurrentItem.OnMouseDown();
        CurrentItem = null;
        UpdateIcon();
    }
}
