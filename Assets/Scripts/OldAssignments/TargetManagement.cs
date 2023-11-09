using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    public GameObject knight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == knight.gameObject.name)
        {
            float x,y,z;
            // move target (this) to a new position
            x = Random.Range(10,300);
            z = Random.Range(10,250);
            y = Terrain.activeTerrain.SampleHeight(new Vector3(x,0,z));
            this.transform.position = new Vector3(x,y,z);
        }
    }
}
