using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Door : Portal, IInteractable
{
    [SerializeField] private Transform textSpot;
    private PopupText popupText;

    public void Activate()
    {
        StartCoroutine(Transition());
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        PopupText.Instance.Setup(textSpot, "Дверь");
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        PopupText.Instance.Hide();
    }
}
