using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] protected int spawnPoint;
    [SerializeField] protected Location destination;

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject && Player.instance.enabled)
        {
            Map.Instance.OpenLocation(destination, spawnPoint);
        }
    }
}
