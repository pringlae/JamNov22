using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.CanInteract(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            Player.instance.CanNotInteract();
        }
    }
}
