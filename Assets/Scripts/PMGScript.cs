using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PMGScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Mesh";

        mesh.vertices = new Vector3[] {
            Vector3.zero, Vector3.up, Vector3.right
        };
        mesh.triangles = new int[] {
            0,1,2
        };
        mesh.normals = new Vector3[] {
            Vector3.back,Vector3.back,Vector3.back
        };

        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
