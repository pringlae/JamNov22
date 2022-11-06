using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public int ItemId;

    private Vector3 _screenPoint, _offset;
    private Vector3 _startScale;
    private bool _isDragging = false;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    public void OnMouseDown()
    {
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        transform.localScale = _startScale * 1.2f;
        _isDragging = true;
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
