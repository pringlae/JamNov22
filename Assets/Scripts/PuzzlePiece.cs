using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IPointerDownHandler
{
    public Sprite Sprite { get; private set; }
    public RectTransform rectTransform { get; private set; }

    private Vector3 _screenPoint, _offset;
    private Vector3 _startScale;
    private bool _isDragging = false;

    private void Awake()
    {
        _startScale = transform.localScale;
        Sprite = GetComponent<Image>().sprite;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        transform.localScale = _startScale * 1.03f;
        _isDragging = true;
        transform.SetAsLastSibling();
    }

    void Update()
    {
        if (_isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
            if (Input.GetMouseButtonUp(0))
            {
                OnDrop();
            }
        }
    }

    void OnDrop()
    {
        transform.localScale = _startScale;
        _isDragging = false;
    }
}
