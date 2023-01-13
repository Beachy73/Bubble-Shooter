using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string mainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenu);
        }
    }








    //public static LevelManager instance;
    //public List<Material> bubbles = new List<Material>();
    //public GameObject tile;
    //public int xSize;
    //public int ySize;

    //private GameObject[,] tiles;

    //public bool isShifting { get; set; }

    //Tile[] tList;

    //BoundingCircle[] boundingCircle;

    //private Tile[] allTiles;
    //private bool collidersCreated = false;

    //Start is called before the first frame update
    //void Start()
    //{
    //    instance = GetComponent<LevelManager>();

    //    Vector2 offset = PrefabManager.Instance.tilePrefab.GetComponent<MeshRenderer>().bounds.size;
    //    CreateBoard(offset.x, offset.y);

    //    tList = (Tile[])GameObject.FindObjectsOfType<Tile>();



    //    tile = FindObjectOfType<Tile>();
    //    tile.boundingCircle = new BoundingCircle(transform.position, tile.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);

    //    foreach (Tile t in tList)
    //    {
    //        t.boundingCircle = new BoundingCircle(transform.position, t.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);
    //        Debug.Log("Collider Created");
    //    }
    //}

    //private void Update()
    //{
    //    foreach (Tile t in tList)
    //    {
    //        if (Input.GetKeyDown(KeyCode.C))
    //        {
    //            Debug.Log("Clearing all matches");

    //            t.ClearAllMatches();
    //        }

    //        t.boundingCircle.centrePoint = transform.position;

    //    }

    //    foreach (Tile tile in allTiles)
    //    {
    //        Debug.Log("Collected Tiles from Level Manager");

    //        if (collidersCreated == false)
    //        {
    //            tile.boundingCircle = new BoundingCircle(transform.position, tile.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);
    //            collidersCreated = true;

    //            Debug.Log("Colliders Created");
    //            Debug.Log("Bounding Circle Centre Point = " + tile.boundingCircle.centrePoint);
    //        }

    //        tile.boundingCircle.centrePoint = transform.position;
    //    }

    //    foreach (Tile t in tList)
    //    {
    //        //Debug.Log("Bounding Circle Centre Point = " + t.boundingCircle.centrePoint);

    //        if (Input.GetKeyDown(KeyCode.V))
    //        {
    //            Debug.Log("Cleared Matches");
    //            t.ClearAllMatches(t);
    //        }
    //    }
    //}

    //private void CreateBoard(float xOffset, float yOffset)
    //{
    //    tiles = new GameObject[xSize, ySize];

    //    float startX = transform.position.x;
    //    float startY = transform.position.y;

    //    Material[] previousLeft = new Material[ySize];
    //    Material previousBelow = null;

    //    for (int x = 0; x < xSize; x++)
    //    {
    //        for (int y = 0; y < ySize; y++)
    //        {
    //            GameObject newTile = Instantiate(PrefabManager.Instance.tilePrefab, new Vector3(startX + (xOffset * x + ((y % 2) * (xOffset * 0.5f))), startY + (yOffset * y), 0), PrefabManager.Instance.tilePrefab.transform.rotation);
    //            tiles[x, y] = newTile;

    //            newTile.transform.parent = transform;
    //            Material newMaterial = bubbles[Random.Range(0, bubbles.Count)];
    //            newTile.GetComponent<MeshRenderer>().material = newMaterial;
    //        }
    //    }

    //    allTiles = (Tile[])GameObject.FindObjectsOfType(typeof(Tile));
    //}

    //private void CreateColliders()
    //{

    //}

}
