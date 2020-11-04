using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketType
{
    GOOD,
    BAD
}

public class MoveToServer : MonoBehaviour
{
    public PacketType packetType;
    public float speed = 3.0f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameController.Instance.server.transform.position, speed * Time.deltaTime);
    }
}
