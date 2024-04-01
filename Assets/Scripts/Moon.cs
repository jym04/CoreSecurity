using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public float tilteAxis;
    public float moveSpeed;

    private Transform earth;

    private void Awake()
    {
        earth = GameObject.Find("Earth_Space").transform;
    }
    private void Update()
    {
        Revolution();
    }

    void Revolution()
    {
        Vector3 orbitAxis = Quaternion.Euler(tilteAxis, 0f, 0f) * Vector3.up;
        transform.RotateAround(earth.position, orbitAxis, moveSpeed * Time.deltaTime);
    }
}
