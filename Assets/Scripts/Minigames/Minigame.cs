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
        Object.Instantiate(this)._onCompleted = onCompleted;
    }

    protected virtual void Awake()
    {
        _success = false;
        MinigameCanvas.instance.Setup(Close);
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
