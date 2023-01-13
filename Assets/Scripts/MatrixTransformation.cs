using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class MatrixTransformation : MonoBehaviour
{
    public Mesh baseMesh;
    private Mesh mesh;
    private Vector3[] modelSpaceVertices;

    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = new Vector3(1, 1, 1);

    private Matrix4by4 rollMatrix;
    private Matrix4by4 pitchMatrix;
    private Matrix4by4 yawMatrix;

    private Matrix4by4 translationMatrix;
    private Matrix4by4 rotationMatrix;
    private Matrix4by4 scaleMatrix;


    // Start is called before the first frame update
    void Start()
    {
        // Run Code in Editor view - half works

#if UNITY_EDITOR
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh meshCopy = Mesh.Instantiate(mf.sharedMesh) as Mesh;
        mesh = mf.sharedMesh = meshCopy;
#else
        mesh = GetComponent<MeshFilter>().mesh;
#endif
        
        modelSpaceVertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        rollMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Mathf.Deg2Rad * rotation.z), Mathf.Sin(Mathf.Deg2Rad * rotation.z), 0),
            new Vector3(-Mathf.Sin(Mathf.Deg2Rad * rotation.z), Mathf.Cos(Mathf.Deg2Rad * rotation.z), 0),
            new Vector3(0, 0, 1),
            Vector3.zero);

        pitchMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(Mathf.Deg2Rad * rotation.x), Mathf.Sin(Mathf.Deg2Rad * rotation.x)),
            new Vector3(0, -Mathf.Sin(Mathf.Deg2Rad * rotation.x), Mathf.Cos(Mathf.Deg2Rad * rotation.x)),
            Vector3.zero);

        yawMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Mathf.Deg2Rad * rotation.y), 0, -Mathf.Sin(Mathf.Deg2Rad * rotation.y)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(Mathf.Deg2Rad * rotation.y), 0, Mathf.Cos(Mathf.Deg2Rad * rotation.y)),
            Vector3.zero);


        translationMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(position.x, position.y, position.z));

        rotationMatrix = yawMatrix * (pitchMatrix * rollMatrix);

        scaleMatrix = new Matrix4by4(
            new Vector3(1, 0, 0) * scale.x,
            new Vector3(0, 1, 0) * scale.y,
            new Vector3(0, 0, 1) * scale.z,
            Vector3.zero);


        // Matrix Transformation

        Vector3[] transformedVertices = new Vector3[modelSpaceVertices.Length];

        Matrix4by4 M = translationMatrix * (rotationMatrix * scaleMatrix);

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = M * modelSpaceVertices[i];
        }

        mesh.vertices = transformedVertices;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}

