using UnityEngine;

[RequireComponent(typeof(InteractTarget))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] protected int spawnPoint;
    [SerializeField] protected Location destination;

    public virtual void Activate()
    {
        Map.Instance.OpenLocation(destination, spawnPoint);
    }
}
