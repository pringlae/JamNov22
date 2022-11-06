using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour 
{
    static List<Fish> allFishes = new List<Fish>();

    public Vector3 patrolExtents;
    protected Vector3 patrolCenterPoint;

    public float patrolSpeed = 1;
    public float forwardRotation = 1;
    public Vector3 swingRange;
    public Vector3 swingSpeed;
    public bool ReadyToBeHooked { get; private set; } = false;

    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected SpriteRenderer sprite;

    protected Vector3 nextDestination;
    Vector2 forwardDirection;
    Vector3 swing;
    protected float currentHealth;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        patrolCenterPoint = transform.position;
        patrolCenterPoint.z = 0;
    }

    void Start()
    {
        GenerateNewDestination();
    }

    bool isRight;
    float y, z;
    protected virtual void Update()
    {
        forwardDirection = (nextDestination - transform.position).normalized;

        isRight = (nextDestination.x > transform.position.x);
        y = Mathf.MoveTowards(y, isRight ? 180 : 0, 500 * Time.deltaTime);
        z = Mathf.MoveTowards(z, Vector2.SignedAngle(isRight ? Vector3.right : -Vector3.right, forwardDirection), forwardRotation);

        swing.Set(
            swingRange.x * Mathf.Sin(Time.time * swingSpeed.x),
            swingRange.y * Mathf.Sin(Time.time * swingSpeed.y),
            swingRange.z * Mathf.Sin(Time.time * swingSpeed.z)
        );

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(swing.x, swing.y + y, swing.z + (isRight ? -z : z)), forwardRotation);
        
        rigidbody.velocity += patrolSpeed * forwardDirection * Time.deltaTime;

        if (Hook.instance.SeemsInteresting && Vector3.Distance(Hook.instance.transform.position, transform.position) < 7)
        {
            ReadyToBeHooked = true;
            nextDestination = Hook.instance.transform.position;
        }
        else if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            ReadyToBeHooked = false;
            GenerateNewDestination();
        }
    }

    protected void GenerateNewDestination()
    {
        nextDestination = patrolCenterPoint + new Vector3(
            patrolExtents.x * (UnityEngine.Random.value - 0.5f),
            patrolExtents.y * (UnityEngine.Random.value - 0.5f),
            0
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(patrolCenterPoint, patrolExtents);
    }
}
