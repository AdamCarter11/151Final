using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] [Range(0.01f, 1f)] private float smoothSpeed = 0.125f;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        if(target != null){
            offset = transform.position - target.position;
        }
        
    }

    private void LateUpdate() {
        if(target != null){
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
        }
        
    }
}
