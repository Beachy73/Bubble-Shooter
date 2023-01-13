using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootBubble : MonoBehaviour
{
    public BubbleSpawner bubbleSpawner;
    public NextBubble nextBubble;
    public BoundingCircle boundingCircle;
    public SnapToGrid snapToGrid;
    public HexGrid hexGrid;
    public GameObject lowerBoundary;

    private DrawDirectionLine directionLine;
    private Vector3 newPos;
    private Material newMaterial;
    private LevelWall[] levelWalls;
    private Tile[] otherBubbles;

    public float moveSpeed = 10.0f;
    [HideInInspector]
    public Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 bounceVelocity = new Vector3(0, 0, 0);
    private Vector3 pos = new Vector3(0, 0, 0);

    [HideInInspector]
    public bool bubbleFired = false;
    private bool hitWall = false;
    private bool firstBounce = true;
    private bool secondBounce = false;
    private bool bubblesCollected = false;
    
    public string gameOverScene;
    public string victory;

    [HideInInspector]
    public int moveGridDown = 0;
    public int gridDownTurnCount = 8;

    //RaycastHit2D hit;


    private void Awake()
    {
        levelWalls = (LevelWall[])GameObject.FindObjectsOfType(typeof(LevelWall));
    }

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        directionLine = GetComponent<DrawDirectionLine>();

        pos = transform.position;
        velocity = directionLine.direction;
        bubbleFired = false;

        boundingCircle = new BoundingCircle(pos, meshRenderer.bounds.size.x * 0.5f);

        StartCoroutine(CollectBubbles());
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        boundingCircle.centrePoint = transform.position;

        newMaterial = bubbleSpawner.currentSprite;

        if (bubblesCollected == false)
        {
            StartCoroutine(CollectBubbles());
        }

        if ((Input.GetKey(KeyCode.Space)) || (bubbleFired == true))
        {
            velocity = GetVelocity();

            FireBubble();
        }

        foreach (LevelWall levelWall in levelWalls)
        {
            //Debug.Log("Level Walls collected successfully");

            if (boundingCircle.IntersectWithAABB(boundingCircle, levelWall.boundingBox) == true)
            {
                //Debug.Log("Collision!");

                if (levelWall.isLevelTop == true)
                {
                    bubbleFired = false;
                    return;
                }

                StartCoroutine(BounceOffVerticalWall());
            }
        }

        if (bubblesCollected == true)
        {
            if (otherBubbles.Length <= 0)
            {
                SceneManager.LoadScene(victory);
                return;
            }

            foreach (Tile tile in otherBubbles)
            {
                //Debug.Log("Each bubble centre point: " + tile.boundingCircle.centrePoint);
                //Debug.Log("Tiles collected successfully");

                if (boundingCircle.Intersects(tile.boundingCircle) == true)
                {
                    Debug.Log("Collision!");

                    bubbleFired = false;

                    secondBounce = false;

                    newPos = snapToGrid.SnapToPos(transform.position, tile.boundingCircle);

                    if (newPos.y < lowerBoundary.transform.position.y)
                    {
                        Debug.Log("Game Over!");
                        SceneManager.LoadScene(gameOverScene);
                        return;
                    }

                    PlaceNewBubble();
                }

                if (tile.transform.position.y < lowerBoundary.transform.position.y)
                {
                    Debug.Log("Game Over!");
                    SceneManager.LoadScene(gameOverScene);
                    return;
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Material tempMaterial;

            tempMaterial = bubbleSpawner.currentSprite;

            bubbleSpawner.currentSprite = nextBubble.nextSprite;
            nextBubble.nextSprite = tempMaterial;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (bubbleSpawner.meshRenderer.enabled == true)
            {
                bubbleSpawner.meshRenderer.enabled = false;
            }
            else
            {
                bubbleSpawner.meshRenderer.enabled = true;
            }
        }
    }

    private Vector3 GetVelocity()
    {
        Vector3 getVelocity = directionLine.direction;

        return getVelocity;
    }

    public void FireBubble()
    {
        if (hitWall == false)
        {
            velocity = VectorMaths.VectorNormalized(velocity);
            velocity = VectorMaths.MultiplyVector(velocity, Time.deltaTime);
            velocity = VectorMaths.MultiplyVector(velocity, moveSpeed);

            pos.x += velocity.x;
            pos.y += velocity.y;
        }
        else if (hitWall == true)
        {
            bounceVelocity = VectorMaths.VectorNormalized(bounceVelocity);
            bounceVelocity = VectorMaths.MultiplyVector(bounceVelocity, Time.deltaTime);
            bounceVelocity = VectorMaths.MultiplyVector(bounceVelocity, moveSpeed);

            pos.x += bounceVelocity.x;
            pos.y += bounceVelocity.y;
        }

        transform.position = pos;

        bubbleFired = true;
    }

    //private void BounceOffWall()
    //{
    //    Debug.Log("Previous velocity: " + velocity);

    //    Vector3 wallNormal = new Vector3(1, 1, 1);
        
    //    if (hit.collider != null)
    //    {
    //        wallNormal = new Vector3(hit.normal.x, hit.normal.y, 0);
    //    }

    //    Vector3 newVelocity = Vector3.zero;

    //    Vector3 u = (VectorMaths.VectorDotProduct(velocity, wallNormal) * wallNormal);
    //    Vector3 w = velocity - u;

    //    //Debug.Log("n . n = " + VectorMaths.VectorDotProduct(wallNormal, wallNormal));

    //    newVelocity = w - u;
    //    bounceVelocity = newVelocity;

    //    Debug.Log("Wall Normal = " + wallNormal);
    //    Debug.Log("New velocity: " + bounceVelocity);
    //}

    private IEnumerator BounceOffVerticalWall()
    {
        if (firstBounce == true)
        {
            bounceVelocity.x = -velocity.x;
            bounceVelocity.y = velocity.y;
            bounceVelocity.z = velocity.z;
            hitWall = true;
            firstBounce = false;

            yield return new WaitForSeconds(0.5f);
            secondBounce = true;
        }
        else if (secondBounce == true)
        {
            hitWall = false;
            secondBounce = false;

            yield return new WaitForSeconds(0.5f);
            firstBounce = true;
        }
    }

    private IEnumerator CollectBubbles()
    {
        yield return new WaitForSeconds(0.01f);

        otherBubbles = (Tile[])GameObject.FindObjectsOfType(typeof(Tile));
        bubblesCollected = true;
    }

    private void PlaceNewBubble()
    {
        GameObject newBubble = Instantiate(PrefabManager.Instance.tilePrefab, newPos, PrefabManager.Instance.tilePrefab.transform.rotation, hexGrid.transform);
        newBubble.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
        BoundingCircle newBoundingCircle = new BoundingCircle(newBubble.transform.position, GetComponent<MeshRenderer>().bounds.size.x * 0.5f);

        StartCoroutine(ResetBubble());

        nextBubble.RandomSprite();
        bubbleSpawner.SwapBubble();

        Tile newTile = newBubble.GetComponent<Tile>();
        newTile.ClearAllMatches(newBubble);

        MoveGridDown();
    }

    private IEnumerator ResetBubble()
    {
        transform.position = bubbleSpawner.spawnPos;
        bubbleFired = false;
        hitWall = false;
        firstBounce = true;

        bubblesCollected = false;

        // Not sure why this works but it does
        yield return new WaitForSeconds(0.8f);
        secondBounce = false;

        // Second bounce always being set to true - check what happens after this function

    }

    private void MoveGridDown()
    {
        moveGridDown++;
        Debug.Log("Move Grid Down Turn = " + moveGridDown);

        if (moveGridDown >= gridDownTurnCount)
        {
            Vector3 pos = hexGrid.transform.position;
            pos.y -= hexGrid.yOffset;
            hexGrid.transform.position = pos;

            foreach (LevelWall levelWall in levelWalls)
            {
                if (levelWall.isLevelTop == true)
                {
                    Vector3 wallPos = levelWall.transform.position;
                    wallPos.y -= hexGrid.yOffset;
                    levelWall.transform.position = wallPos;
                }
            }

            moveGridDown = 0;
        }
    }
}
