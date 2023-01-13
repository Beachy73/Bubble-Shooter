using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MeshRenderer render;
    private GameObject firedBubble;
    private List<GameObject> matchingTiles;
    private List<GameObject> newMatchingTiles;
    private ShootBubble shootBubble;

    public BoundingCircle boundingCircle;

    private float xFireDirection = 0.35f;
    private float yFireDirection = 0.6f;
    
    private bool matchFound = false;


    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        shootBubble = FindObjectOfType<ShootBubble>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boundingCircle = new BoundingCircle(transform.position, render.bounds.size.x * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        boundingCircle.centrePoint = transform.position;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        newMatchingTiles = new List<GameObject>();
        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir, 1f);
        Debug.DrawRay(transform.position, castDir, Color.red, 1f);

        if ((hit.collider != null) && (hit.collider.GetComponent<MeshRenderer>().sharedMaterial == render.sharedMaterial))
        {
            newMatchingTiles.Add(firedBubble);
            Debug.Log("Fired bubble added to the list");
            raycastHits.Add(hit);
            newMatchingTiles.Add(hit.collider.gameObject);

            for (int i = 0; i < newMatchingTiles.Count; i++)
            {
                newMatchingTiles[i].layer = 2;
            }
        }

        while (raycastHits.Count > 0)
        {
            Vector2[] newCastDir = new Vector2[6]
            {
                Vector2.left, Vector2.right,
                new Vector2(xFireDirection, yFireDirection), new Vector2(-xFireDirection, yFireDirection),
                new Vector2(xFireDirection, -yFireDirection), new Vector2(-xFireDirection, -yFireDirection)
            };

            RaycastHit2D newHit = raycastHits[0];

            for (int i = 0; i < newCastDir.Length; i++)
            {

                newHit = Physics2D.Raycast(raycastHits[0].transform.position, newCastDir[i], 1f);
                Debug.DrawRay(raycastHits[0].transform.position, newCastDir[i], Color.white, 50f);
                
                if ((newHit.collider != null) && (newHit.collider.GetComponent<MeshRenderer>().sharedMaterial == render.sharedMaterial))
                {
                    newHit.collider.gameObject.layer = 2;

                    newMatchingTiles.Add(newHit.collider.gameObject);
                    raycastHits.Add(newHit);
                }
            }

            raycastHits.Remove(raycastHits[0]);
        }       

        return newMatchingTiles;
    }

    private void ClearMatch(Vector2[] paths)
    {
        matchingTiles = new List<GameObject>();

        for (int i = 0; i < paths.Length; i++)
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }

        if (matchingTiles.Count >= 3)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                Debug.Log("Matching tiles count = " + matchingTiles.Count);
                Debug.Log("Matching Tiles pos: " + matchingTiles[i].transform.position);


                matchingTiles[i].GetComponent<MeshRenderer>().enabled = false;
            }
            matchFound = true;
        }
        else
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].layer = 0;
            }
        }
    }

    public void ClearAllMatches(GameObject bubble)
    {
        if (render.enabled == false)
            return;

        firedBubble = bubble;

        ClearMatch(new Vector2[6]
        {
            Vector2.left, Vector2.right,
            new Vector2(xFireDirection, yFireDirection), new Vector2(-xFireDirection, yFireDirection),
            new Vector2(xFireDirection, -yFireDirection), new Vector2(-xFireDirection, -yFireDirection)
        });

        if (matchFound)
        {
            render.enabled = false;

            if (render.enabled == false)
            {
                Destroy(firedBubble);
                //ScoreManager.currentScore += ScoreManager.scoreAmount;

                for (int i = 0; i < matchingTiles.Count; i++)
                {
                    Debug.Log("Destroy!");
                    Destroy(matchingTiles[i]);

                    ScoreManager.currentScore += ScoreManager.scoreAmount;
                }
                
            }

            shootBubble.moveGridDown -= 1;
            matchFound = false;
        }
    }
}

