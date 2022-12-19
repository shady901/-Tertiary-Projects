using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMeshReseter : MonoBehaviour
{
    //Created by alex
    //simple issue we had with a mesh render that kept the old generated map that this fixes
    //object data
    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(mesh.GetComponent<MeshCollider>());

        mesh.AddComponent<MeshCollider>();


    }

}
