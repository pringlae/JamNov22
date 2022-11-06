using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float sizeX => Camera.main.orthographicSize / Screen.height * Screen.width;
    private float sizeY => Camera.main.orthographicSize;

    void Update()
    {
        Vector3 newPos = new Vector3(
            Player.instance.transform.position.x,
            Player.instance.transform.position.y + sizeY - 1,
            -10
        );
        if (Map.Instance.CurrentLocation != null && Map.Instance.CurrentLocation.CameraBounds != null)
        {
            var bounds = Map.Instance.CurrentLocation.CameraBounds.bounds;
            if (newPos.x - sizeX < bounds.min.x)
                newPos.x = bounds.min.x + sizeX;
            else if (newPos.x + sizeX > bounds.max.x)
                newPos.x = bounds.max.x - sizeX;
            if (newPos.y - sizeY < bounds.min.y)
                newPos.y = bounds.min.y + sizeY;
            else if (newPos.y + sizeY > bounds.max.y)
                newPos.y = bounds.max.y - sizeY;
        }
        transform.position = newPos;
    }
}
