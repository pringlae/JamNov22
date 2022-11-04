using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Id;

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
 
    void OnMouseDown()
    {
        Debug.Log("here");
        _rigidbody.isKinematic = true;
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if(hit != null){
                Debug.Log("Hit Collider: " + hit.transform.name);
            }
            else {
                Debug.Log("No colliders hit from mouse click");
            }
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        _rigidbody.isKinematic = false;
    }
}
