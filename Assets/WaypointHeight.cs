using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHeight : MonoBehaviour
{
    private float terrainY;
    // Start is called before the first frame update
    void Start()
    {
        terrainY = Terrain.activeTerrain.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainY + 1f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
