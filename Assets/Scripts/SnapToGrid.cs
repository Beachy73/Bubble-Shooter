using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    private HexGrid hexGrid;

    private float width;
    private float height;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hexGrid = FindObjectOfType<HexGrid>();
    }

    
    public Vector3 SnapToPos(Vector3 firedBubble, BoundingCircle collidedBubble)
    {
        Vector3 snappedPosition = Vector3.zero;

        Vector3 centrePoint = collidedBubble.centrePoint;
        float halfWidth = collidedBubble.radius;
        height = (collidedBubble.radius * 2f) * 1.14f;


        bool leftSnap = false;
        bool rightSnap = false;

        if (firedBubble.x > centrePoint.x)
        {
            snappedPosition.x = centrePoint.x + (halfWidth);
            //Debug.Log("Snapped Right");
            rightSnap = true;
        }
        else if (firedBubble.x < centrePoint.x)
        {
            snappedPosition.x = centrePoint.x - (halfWidth);
            //Debug.Log("Snapped Left");
            leftSnap = true;
        }

        if ((firedBubble.y < (centrePoint.y + (height * 0.25f))) && (firedBubble.y > (centrePoint.y - (height * 0.25f))))
        {
            snappedPosition.y = centrePoint.y;
            //Debug.Log("& Snapped Level");

            if (rightSnap)
            {
                snappedPosition.x += halfWidth;
            }
            else if (leftSnap)
            {
                snappedPosition.x -= halfWidth;
            }
        }
        else if (firedBubble.y < centrePoint.y - (height * 0.25f))
        {
            snappedPosition.y = (centrePoint.y - (height * 0.75f));
            //Debug.Log("& Snapped Bottom");
        }
        else if (firedBubble.y > centrePoint.y + (height * 0.25f))
        {
            snappedPosition.y = (centrePoint.y + (height * 0.75f));
            //Debug.Log("& Snapped Top");
        }

        leftSnap = false;
        rightSnap = false;

        return snappedPosition;
    }
}
