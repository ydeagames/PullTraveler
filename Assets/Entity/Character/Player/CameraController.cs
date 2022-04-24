using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public float maxSpeed = float.PositiveInfinity;

    // 現在速度(SmoothDampの計算のために必要)
    private Vector2 _currentVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 camPosition = camTransform.position;
        var pos = Vector2.SmoothDamp(
            camPosition,
            transform.position,
            ref _currentVelocity,
            smoothTime,
            maxSpeed);
        camPosition.x = pos.x;
        camPosition.y = pos.y;
        camTransform.position = camPosition;
    }
}
