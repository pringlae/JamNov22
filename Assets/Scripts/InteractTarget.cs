using UnityEngine;

public class InteractTarget : MonoBehaviour
{
    public string Name;
    private IInteractable _interactScript;
    
    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            if (_interactScript == null) _interactScript = GetComponent<IInteractable>();
            Player.instance.CanInteract(_interactScript);
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.CanNotInteract();
        }
    }

    void OnMouseOver()
    {
        PopupText.Instance.Setup(transform, Name);
    }

    void OnMouseExit()
    {
        PopupText.Instance.Hide();
    }
}
