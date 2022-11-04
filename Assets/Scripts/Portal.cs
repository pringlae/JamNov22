using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    enum DestinationIdentifier { A, B, C, D, E }
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(Transition());
        }
    }

    protected IEnumerator Transition()
    {
        if (sceneToLoad < 0) yield break;

        DontDestroyOnLoad(gameObject);

        yield return Fader.Instance.FadeOut(fadeOutTime);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);

        yield return Fader.Instance.FadeIn(fadeInTime);

        Destroy(gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        var player = FindObjectOfType<Player>();
        player.transform.position = otherPortal.spawnPoint.position;
    }

    private Portal GetOtherPortal()
    {
        foreach (var portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destination != destination) continue;
            return portal;
        }
        return null;
    }
}
