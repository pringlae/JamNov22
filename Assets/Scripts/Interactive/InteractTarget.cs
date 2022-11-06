using UnityEngine;

public class InteractTarget : MonoBehaviour
{
    public string Name;
    private IInteractable _interactScript;
    [SerializeField] private SpriteRenderer _highlight;
    private bool _nearBy = false;

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _nearBy = true;
            if (_interactScript == null) _interactScript = GetComponent<IInteractable>();
            Player.instance.CanInteract(_interactScript);
            if (_highlight != null) _highlight.enabled = true;
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _nearBy = false;
            Player.instance.CanNotInteract(_interactScript);
            if (_highlight != null) _highlight.enabled = false;
        }
    }

    void OnMouseOver()
    {
        PopupText.Instance.Setup(transform, Name);
        if (_highlight != null) _highlight.enabled = true;
    }

    void OnMouseExit()
    {
        PopupText.Instance.Hide();
        if (!_nearBy && _highlight != null)
            _highlight.enabled = false;
    }

    void OnEnable()
    {
        if (_highlight != null) _highlight.enabled = false;
    }
}
