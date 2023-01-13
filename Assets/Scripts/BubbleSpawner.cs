using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject nextBubble;

    private int rand;

    [HideInInspector]
    public Material currentSprite;
    [HideInInspector]
    public MeshRenderer meshRenderer;
    private NextBubble nb;

    [HideInInspector]
    public Vector3 spawnPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;

        nb = nextBubble.GetComponent<NextBubble>();

        meshRenderer = GetComponent<MeshRenderer>();

        rand = Random.Range(0, nb.bubbleSprite.Length);
        meshRenderer.sharedMaterial = nb.bubbleSprite[rand];

        currentSprite = meshRenderer.sharedMaterial;
    }
    

    public void SwapBubble()
    {
        currentSprite = nb.nextSprite;

        meshRenderer.sharedMaterial = currentSprite;
    }
}
