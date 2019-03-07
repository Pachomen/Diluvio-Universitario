using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    static public GameObject Camera;
    [Header("Set in Inspector")]
    public GameObject player;

    public float camZ;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera = Vector3.zero;
        camera.x = player.transform.position.x;
        camera.y = player.transform.position.y;
        camera.z = camZ;
        transform.position = camera;
    }
}
