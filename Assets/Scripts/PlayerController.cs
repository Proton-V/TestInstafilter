using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }
    
    public const float Min_Speed = 0f;
    public const float Max_Speed = 100f;
    [Range(Min_Speed, Max_Speed)]
    public float PlayerSpeed = 5f;
    private Rigidbody _rb;

    private Vector2 _startTapPos;
    private GameObject _mapObj;
    private float _playerMapOffset;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mapObj = GameManager.Instance.MapObject;
        _playerMapOffset = _mapObj.transform.position.z - transform.position.z;
    }
    private void Update()
    {
        CheckSwipe();
        AddForceForward();
    }

    private void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTapPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _rb.AddForce((Input.mousePosition.x - _startTapPos.x) * transform.right, ForceMode.Force);
        }
    }
    private void AddForceForward()
    {
        if (_rb.velocity.magnitude < PlayerSpeed)
            _rb.AddForce(transform.forward * PlayerSpeed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {
            _mapObj.transform.position = new Vector3(0,0, transform.position.z + _playerMapOffset);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
