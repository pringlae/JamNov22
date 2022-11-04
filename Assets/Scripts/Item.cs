using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractTarget
{
    public string Id;
    public Sprite Sprite => _spriteRenderer.sprite;
    public Color SpriteColor => _spriteRenderer.color;

    private static Material _spriteOnTopMat = null, _defaultMat = null;
    private Vector3 _screenPoint, _offset;
    private Vector3 _startScale;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private bool _isDragging = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startScale = transform.localScale;
    }
 
    public void OnMouseDown()
    {
        _rigidbody.isKinematic = true;
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        transform.localScale = _startScale * 2;
        Inventory.Instance.SetHighlighted(Inventory.Instance.MouseOnInventory);
        _isDragging = true;
    }

    void Update()
    {
        if (_isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
            Inventory.Instance.SetHighlighted(Inventory.Instance.MouseOnInventory);
            if (Input.GetMouseButtonUp(0))
            {
                OnDrop();
            }
        }
    }

    void OnDrop()
    {
        transform.localScale = _startScale;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector2.zero;
        _isDragging = false;

        if (Inventory.Instance.MouseOnInventory)
            Inventory.Instance.PutItem(this);
    }
}
