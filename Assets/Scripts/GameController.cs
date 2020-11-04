using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Gameobject References")]
    public GameObject server;
    public List<GameObject> clients;
    public Material deactivatedMat;

    [Space]
    [Header("Boolean value for touch detection")]
    public bool tap;

    [Space]
    [Header("Layers to limit the touch distance")]
    public LayerMask floorMask;

    [Space]
    [Header("Public variables")]
    public float spawnRate = 0.2f;


    public float lineWidth; // use the same as you set in the line renderer.

    private CapsuleCollider capsule;
    private Camera cam;
    private bool isLineDeactivated=false;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gObj in clients)
        {
            LineRenderer lr = gObj.GetComponentInChildren<LineRenderer>();
            InitLinePos(lr, gObj);

        }
    }

    

    // Update is called once per frame
    void Update()
    {
        TouchDetect();

        Test();
    }

    private void Test()
    {
        int randomSpawnPoint = Random.Range(0, clients.Count);


        GameObject gObj = PoolManager.Instance.SpawnInWorld("Virus", clients[randomSpawnPoint].transform.position, clients[randomSpawnPoint].transform.rotation);
    }

    private void InitLinePos(LineRenderer lr, GameObject gObj)
    {
        lineWidth = lr.startWidth;

        capsule = gObj.transform.GetChild(0).gameObject.AddComponent<CapsuleCollider>();
        capsule.radius = lineWidth / 2;
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis for easier "LookAt" orientation

        lr.SetPosition(0, gObj.transform.position);
        lr.SetPosition(1, server.transform.position);

        capsule.transform.position = gObj.transform.position + (server.transform.position - gObj.transform.position) / 2;
        capsule.transform.LookAt(gObj.transform.position);
        capsule.height = (server.transform.position - gObj.transform.position).magnitude;

    }


    private void TouchDetect()
    {
        tap = false;

        if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase==TouchPhase.Began)
            {
                Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit hit;

                if(Physics.Raycast(ray,out hit,floorMask))
                {
                    tap = true;

                    if(hit.collider.tag=="Wire")
                    {
                        isLineDeactivated = true;
                        LineRenderer lr = hit.collider.GetComponent<LineRenderer>();
                        lr.material = deactivatedMat;
                        Debug.Log(hit.collider.name);
                    }
                }
            }
        }
    }
}
