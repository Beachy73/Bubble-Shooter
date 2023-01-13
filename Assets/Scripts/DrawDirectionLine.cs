using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDirectionLine : MonoBehaviour
{
    private GameObject bubbleSpawner;
    private LineRenderer directionLine;
    private Vector2 lineEnd;

    public float lineMoveSpeed = 3.0f;
    public float leftWallBoundary = -7.0f;
    public float rightWallBoundary = 7.0f;

    [HideInInspector]
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        bubbleSpawner = this.gameObject;

        directionLine = GetComponent<LineRenderer>();

        // Line Renderer Settings
        directionLine.startWidth = 0.1f;
        directionLine.endWidth = 0.1f;
        directionLine.positionCount = 2;
        directionLine.startColor = Color.white;
        directionLine.endColor = Color.white;

        lineEnd = new Vector2(bubbleSpawner.transform.position.x, bubbleSpawner.transform.position.y + 2.0f);

        directionLine.SetPosition(0, new Vector3(bubbleSpawner.transform.position.x, bubbleSpawner.transform.position.y, bubbleSpawner.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        direction = VectorMaths.SubtractVectors(directionLine.GetPosition(1), directionLine.GetPosition(0));

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            lineEnd.x -= lineMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            lineEnd.x += lineMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            lineEnd.x = 0.0f;
        }

        lineEnd.x = Mathf.Clamp(lineEnd.x, leftWallBoundary, rightWallBoundary);

        directionLine.SetPosition(1, lineEnd);
    }
}
