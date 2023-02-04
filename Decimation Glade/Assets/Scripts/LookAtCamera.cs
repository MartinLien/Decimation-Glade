using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _target = null;

    private void Start()
    {
        _target = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_target, Vector3.up);
    }
}
