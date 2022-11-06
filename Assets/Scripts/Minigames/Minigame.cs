using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{
    protected System.Action<bool> _onCompleted;
    protected bool _success;

    public virtual void Launch(System.Action<bool> onCompleted)
    {
        var instance = Object.Instantiate(this, MinigameCanvas.instance.transform);
        instance._onCompleted = onCompleted;
        instance.transform.SetAsFirstSibling();
        instance.StartMinigame();
    }

    protected virtual void StartMinigame()
    {
        _success = false;
        MinigameCanvas.instance.Setup(this);
        Player.instance.gameObject.SetActive(false);
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    public virtual void Close()
    {
        _onCompleted.Invoke(_success);
        Player.instance.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
