using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersections : MonoBehaviour
{
    // used to draw lines between intersecting shapes when the button is pressed
    public GameObject linePrefab;
    // list of active lines
    private List<LineRenderer> activeLines = new List<LineRenderer>();
    // list of shapes
    public List<Shape> shapes = new List<Shape>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            shapes.Add(transform.GetChild(i).GetComponent<Shape>());
        }
    }

    /// <summary>
    /// Formats the interactions dictionary to a string, and outputs the string to the console.
    /// </summary>
    public void IntersectionsToConsole()
    {
        // clear lines ready for new ones
        ClearLines();
        // get dictionary of intersection data
        Dictionary<int, List<int>> intersectionDictionary = FindIntersections(shapes);
        // set up string for console output
        string consoleString = "";

        // for each key in dictionary
        foreach (KeyValuePair<int, List<int>> entry in intersectionDictionary)
        {
            // add the entry to the console string
            consoleString += entry.Key.ToString();
            // add the other necessary characters to the console string
            consoleString += " -> (";
            // set up entry counter
            int entryCount = 0;

            // for each value in entry
            foreach (int value in entry.Value)
            {
                // if not the first entry, add a comma to the console string
                if (entryCount > 0)
                {
                    consoleString += ",";
                }
                // add the entry value to the string
                consoleString += value.ToString();
                // increment the count
                entryCount++;
            }
            consoleString += "), ";
        }
        // output the string with all dictionary info to the console
        Debug.Log(consoleString);
    }

    /// <summary>
    /// Assembles and returns the dictionary of intersections.
    /// </summary>
    /// <param name="shapes">The list of shapes to find the intersections of.</param>
    /// <returns>The returned dictionary of intersections.</returns>
    public Dictionary<int, List<int>> FindIntersections(List<Shape> shapes)
    {
        // create new dictionary to hold intersection data
        Dictionary<int, List<int>> intersectionDictionary = new Dictionary<int, List<int>>();

        for (int i = 0; i < shapes.Count; i++) // for each shape
        {
            // create new list to hold IDs of intersecting shapes
            List<int> intersectingShapes = new List<int>();

            // get other shape to assess
            for (int ii = 0; ii < shapes.Count; ii++) 
            {
                // so long as its not its self
                if (ii != i) 
                {
                    // if the two shapes intersect
                    if (Intersects(shapes[i], shapes[ii])) 
                    {
                        // add to list of intersecting shapes
                        intersectingShapes.Add(shapes[ii].ID);
                        // draw a line between the two shapes to indicate that they intersect
                        DrawLine(shapes[i].transform.position, shapes[ii].transform.position);
                    }
                }
            }

            // add new entry to the dictionary
            intersectionDictionary.Add(shapes[i].ID, intersectingShapes);
        }

        // return the filled dictionary
        return intersectionDictionary;
    }

    /// <summary>
    /// Figures out whether the two given shapes interact or not, returns the outcome as a bool.
    /// </summary>
    /// <param name="theCurrentShape">The inital shape being assessed.</param>
    /// <param name="theOtherShape">The other shape being assessed.</param>
    /// <returns></returns>
    bool Intersects(Shape theCurrentShape, Shape theOtherShape)
    {
        // if both shapes are circles
        if (theCurrentShape.shapeType == Shape.ShapeType.circle && theOtherShape.shapeType == Shape.ShapeType.circle)
        {
            // calculate the distance between the centres of the two circles
            float distanceBetweenCentres = (theCurrentShape.transform.position - theOtherShape.transform.position).magnitude;
            // calculate the sum of the two circles radiuses (the max possible distance for the shapes to be intersecting)
            float maxIntersectDistance = theCurrentShape.xScale / 2 + theOtherShape.xScale / 2;

            // if the distance betwenn the centres is less than the max intersecting distance, then they are intersecting
            if (distanceBetweenCentres < maxIntersectDistance)
            {
                return true;
            }
            else // not intersecting
            {
                return false;
            }
        }
        // if both shapes are rectangles
        else if (theCurrentShape.shapeType == Shape.ShapeType.rectangle && theOtherShape.shapeType == Shape.ShapeType.rectangle)
        {
            // get the distance between centres of the two rectangles, on both axis
            float xDistanceBetweenCentres = CustomAbs(theCurrentShape.transform.position.x - theOtherShape.transform.position.x);
            float yDistanceBetweenCentres = CustomAbs(theCurrentShape.transform.position.y - theOtherShape.transform.position.y);

            // get the max possible distance the shapes can be intersecting, on both axis
            float maxIntersectDistanceX = theCurrentShape.xScale / 2 + theOtherShape.xScale / 2;
            float maxIntersectDistanceY = theCurrentShape.yScale / 2 + theOtherShape.yScale / 2;

            // if the distance between centres is less than the max instersect distance, for both axis, then they are intersecting
            if (xDistanceBetweenCentres < maxIntersectDistanceX && yDistanceBetweenCentres < maxIntersectDistanceY)
            {
                return true;
            }
            else // not intersecting
            {
                return false;
            }
        }
        else
        {
            Shape theRectangle;
            Shape theCircle;

            // if the 'current' is a rectangle, and the other is a circle
            if (theCurrentShape.shapeType == Shape.ShapeType.rectangle)
            {
                // assign which shape is which
                theRectangle = theCurrentShape;
                theCircle = theOtherShape;
            }
            else
            {
                // assign which shape is which
                theRectangle = theOtherShape;
                theCircle = theCurrentShape;
            }

            // find rectngle corners, on each axis seperately
            float rectangleMinXBoundary = theRectangle.transform.position.x - theRectangle.xScale / 2;
            float rectangleMaxXBoundary = theRectangle.transform.position.x + theRectangle.xScale / 2;
            float rectangleMinYBoundary = theRectangle.transform.position.y - theRectangle.yScale / 2;
            float rectangleMaxYBoundary = theRectangle.transform.position.y + theRectangle.yScale / 2;

            // find closest point on the rectangle in each axis
            float clampedCircleX = CustomClamp(theCircle.transform.position.x, rectangleMinXBoundary, rectangleMaxXBoundary);
            float clampedCircleY = CustomClamp(theCircle.transform.position.y, rectangleMinYBoundary, rectangleMaxYBoundary);

            // get circle radius
            float circleRadius = theCircle.xScale/2;

            // get distance from circle centre to closest point on rectangle
            float distanceToClosestPoint = (new Vector3(clampedCircleX, clampedCircleY, 90) - theCircle.transform.position).magnitude;

            // if distance is less than radius, then they are intersecting
            if (distanceToClosestPoint < circleRadius)
            {
                return true;
            }
            else // not intersecting
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Draws a line between two points.
    /// </summary>
    /// <param name="start">The line start positions.</param>
    /// <param name="end">The line end positions.</param>
    void DrawLine(Vector3 start, Vector3 end)
    {
        LineRenderer newLine = Instantiate(linePrefab, transform).GetComponent<LineRenderer>();
        newLine.SetPosition(0, start);
        newLine.SetPosition(1, end);
        activeLines.Add(newLine);
    }

    /// <summary>
    /// Destroys all lines connecting intersecting shapes.
    /// </summary>
    public void ClearLines()
    {
        foreach (var line in activeLines)
        {
            Destroy(line.gameObject);
        }
        activeLines.Clear();
    }

    /// <summary>
    /// Clamp, but not Mathf.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The min output value.</param>
    /// <param name="max">The max output value</param>
    /// <returns>The returned clamped value.</returns>
    public float CustomClamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    /// <summary>
    /// Abs, but not Mathf.
    /// </summary>
    /// <param name="value">The value to Abs</param>
    /// <returns>The returned absolute value.</returns>
    public float CustomAbs(float value)
    {
        if (value < 0) return -value;
        return value;
    }
}
