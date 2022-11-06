using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public static Hook instance;

    [SerializeField] private Vector3 _targetPoint;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float _moveSpeed;

    private float _lastMoveTimer;
    public bool Deactivated { get; private set; } = false;
    public Fish HookedFish { get; private set; } = null;
    
    public bool SeemsInteresting => Time.deltaTime - _lastMoveTimer < 5 && HookedFish == null;
    Rigidbody2D _rigidbody;

    Vector3[] _linePositions = new Vector3[2];

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _linePositions[0] = _targetPoint;
        _lastMoveTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        _linePositions[1] = transform.position + transform.up * 0.4f;
        _line.SetPositions(_linePositions);
        Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);

        if (Deactivated)
        {
            _rigidbody.gravityScale = 0;
            return;
        }
        
        _rigidbody.gravityScale = Mathf.InverseLerp(-10, 15, transform.position.y) * 0.1f;

        if (Input.GetButton("Jump"))
        {
            _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity, _targetPoint - transform.position, Time.deltaTime * _moveSpeed);
            _lastMoveTimer = Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 6)
        {
            Deactivated = true;
        }
        else if (HookedFish == null && collider.TryGetComponent<Fish>(out Fish fish))
        {
            HookedFish = fish;
            fish.enabled = false;
            var joint = gameObject.AddComponent<HingeJoint2D>();
            joint.connectedBody = fish.GetComponent<Rigidbody2D>();
        }
    }
}
