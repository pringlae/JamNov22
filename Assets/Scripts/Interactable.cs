using UnityEngine;

public class Interactable : MonoBehaviour
{
    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.CanInteract(GetComponent<IInteractable>());
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.CanNotInteract();
        }
    }
}
