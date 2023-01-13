using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBubble : MonoBehaviour
{
    public Material[] bubbleSprite;
    private int rand;

    [HideInInspector]
    public Material nextSprite;
    private Material currentSprite;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        RandomSprite();

        nextSprite = meshRenderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        nextSprite = meshRenderer.sharedMaterial;
    }

    public void RandomSprite()
    {
        rand = Random.Range(0, bubbleSprite.Length);

        currentSprite = bubbleSprite[rand];

        meshRenderer.sharedMaterial = currentSprite;
    }
}
