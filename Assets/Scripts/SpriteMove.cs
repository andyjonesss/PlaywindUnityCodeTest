using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    // list of all of the shape transforms
    List<Transform> shapeTransforms = new List<Transform>();

    // tracking of the 'selected' and 'dragged' shapes
    Transform selectedShapeTransform;
    Transform draggedShapeTransform;

    // colours for selected and unselected shapes
    Color selectedColour = new Color(1f, 1f, 1f, 0.6f);
    Color unselectedColour = new Color(0.73f, 1f, 0.73f, 0.6f);

    // rays for mouse-over-shape detection
    Ray ray;
    RaycastHit raycastHit;
    [SerializeField] LayerMask shapeLayerMask;

    // reference to intersectino script, to clear lines when a shape is dragged
    Intersections intersectionsScript;

    void Awake()
    {
        // get reference to intersections script
        intersectionsScript = GameObject.Find("Shapes").GetComponent<Intersections>();

        // assemble the list
        for (int i = 0; i < transform.childCount; i++)
        {
            shapeTransforms.Add(transform.GetChild(i));
        }
    }

    void Update()
    {
        CursorSelect();
        MoveShape();
    }

    /// <summary>
    /// Used to click and drag shapes to new positions on the screen using mouse input.
    /// </summary>
    void MoveShape()
    {
        // when mouse click down
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (selectedShapeTransform) // if cursor hovering over a shape
            {
                // clear lines
                intersectionsScript.ClearLines(); 
                // set this shape as being dragged
                draggedShapeTransform = selectedShapeTransform; 
            }
        }
        // when mouse click up
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Set 'dragged shape' to null
            draggedShapeTransform = null;
        }

        // if a shape is being dragged
        if (draggedShapeTransform)
        {
            // update shapes position based on mouse movement
            Vector3 moveVector = Vector3.zero;            
            moveVector.x = Input.GetAxis("Horizontal");
            moveVector.y = Input.GetAxis("Vertical");
            moveVector = moveVector / 50f;
            draggedShapeTransform.position += moveVector;
        }
    }

    /// <summary>
    /// Used to identify whether the cursor is hovering over a shape, so a shape can be moved.
    /// Changes the colour of the shape where this is the case.
    /// </summary>
    void CursorSelect()
    {        
        // if a shape is not currently being dragged
        if (!draggedShapeTransform)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float maxrayLength = 100;

            // raycast from the cursor, check if hit a shape
            if (Physics.Raycast(ray, out raycastHit, maxrayLength, shapeLayerMask)) 
            {
                // if not hit the same shape as last frame, update 'selected' and colours
                if (raycastHit.transform != selectedShapeTransform) 
                {
                    selectedShapeTransform = raycastHit.transform;

                    for (int i = 0; i < shapeTransforms.Count; i++)
                    {
                        shapeTransforms[i].GetComponent<SpriteRenderer>().color = unselectedColour;
                    }

                    raycastHit.transform.GetComponent<SpriteRenderer>().color = selectedColour;
                }
            }
            else // if hit nothing
            {
                // if a shape was hit last frame, reset 'selected' and colours
                if (selectedShapeTransform != null) 
                {
                    selectedShapeTransform = null;
                    for (int i = 0; i < shapeTransforms.Count; i++)
                    {
                        shapeTransforms[i].GetComponent<SpriteRenderer>().color = unselectedColour;
                    }
                }
            }
        }        
    }
}
