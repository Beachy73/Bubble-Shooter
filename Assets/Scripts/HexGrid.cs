using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int xSize = 8;
    public int ySize = 10;

    public List<Material> bubbles = new List<Material>();

    private float radius;
    [HideInInspector]
    public bool useAsInnerCircleRadius = true;

    [HideInInspector]
    public float xOffset, yOffset;

    
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.currentScore = 0;

        radius = (PrefabManager.Instance.tilePrefab.GetComponent<MeshRenderer>().bounds.size.x) * 0.5f;

        float unitLength = (useAsInnerCircleRadius) ? (radius / (Mathf.Sqrt(3) / 2)) : radius;

        xOffset = unitLength * Mathf.Sqrt(3);
        yOffset = unitLength * 1.5f;

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                Vector2 hexPos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexPos.x, hexPos.y, 0) + transform.position;
                GameObject newBubble = Instantiate(PrefabManager.Instance.tilePrefab, pos, PrefabManager.Instance.tilePrefab.transform.rotation);

                newBubble.transform.parent = transform;
                Material newMaterial = bubbles[Random.Range(0, bubbles.Count)];
                
                newBubble.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
            }
        }
    }

    Vector2 HexOffset(int x, int y)
    {
        Vector2 position = Vector2.zero;

        if (y % 2 == 0)
        {
            position.x = x * xOffset;
            position.y = y * yOffset;
        }
        else
        {
            position.x = (x + 0.5f) * xOffset;
            position.y = y * yOffset;
        }

        return position;
    } 
}
