using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject server;

    public List<GameObject> clients;

    private List<LineRenderer> lines = new List<LineRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gObj in clients)
        {
            LineRenderer lr = gObj.GetComponent<LineRenderer>();

            lines.Add(lr);

            SetLinePostions(lr, gObj);
        }
    }

    private void SetLinePostions(LineRenderer lr, GameObject gObj)
    {
        lr.SetPosition(0, gObj.transform.position);
        lr.SetPosition(1, server.transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
