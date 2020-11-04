using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToServer : MonoBehaviour,IPooledObject
{
    public Transform target;
    public float speed = 1.0f;

    public void OnObjectSpawned()
    {
        Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
