using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector3 _offset;

    private void Start()
    {
        _playerController = PlayerController.Instance;
        _offset = transform.position - _playerController.transform.position;
    }

    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        transform.position = _playerController.transform.position + _offset;
    }
}
