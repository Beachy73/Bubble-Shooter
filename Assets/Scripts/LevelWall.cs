using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWall : MonoBehaviour
{
    public AABB boundingBox;
    private MeshRenderer meshRenderer;

    [HideInInspector]
    public float xSize = 0f;
    [HideInInspector]
    public float ySize = 0f;

    public bool isLevelTop = false;

    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        xSize = meshRenderer.bounds.size.x;
        xSize = (xSize * transform.localScale.x);

        ySize = meshRenderer.bounds.size.y;
        ySize = ySize * transform.localScale.y;

        Vector3 pos = transform.position;

        boundingBox = new AABB(new Vector3(pos.x - xSize, pos.y - (ySize * 0.5f), pos.z),
            new Vector3(pos.x + xSize, pos.y + (ySize * 0.5f), pos.z));
    }
}
