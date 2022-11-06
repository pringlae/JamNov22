using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public int Id;
    public string VisibleName;
    public Transform[] SpawnPoints;
    public float CameraSize = 4;
    public BoxCollider2D CameraBounds;
}
