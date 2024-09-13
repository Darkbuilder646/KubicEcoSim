using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [Space]
    [SerializeField] private float _cameraSizeTarget;

    private Camera _cam;
    private GameObject _camPivot;

    private Vector3 _moveVel;
    private float _cameraSizeVel;
    private Vector3 _cameraPositionTarget;

    private void Start() 
    {
        _cam = Camera.main;
        _camPivot = gameObject;
        _cameraPositionTarget = _camPivot.transform.position; 
        _camPivot.transform.position = _cameraPositionTarget;
    }

    private void LateUpdate() 
    {
        _cameraPositionTarget = _player.transform.position; 
        _camPivot.transform.position = Vector3.SmoothDamp(_camPivot.transform.position, _cameraPositionTarget, ref _moveVel, 0.5f);

        _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _cameraSizeTarget, ref _cameraSizeVel, 0.5f);
    }
}
