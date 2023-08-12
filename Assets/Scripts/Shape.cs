using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a Shape in the scene, which can be of type Circle or Rectangle.
public class Shape : MonoBehaviour
{
    // The type of shape this object represents
    public ShapeType shapeType;

    // Enum to define the types of shapes available
    public enum ShapeType
    {
        circle,      
        rectangle   
    }

    public int ID;  // unique identifier for the shape

    // scaling values for the shape along the x and y axes
    public float xScale;
    public float yScale;

    private void Awake()
    {
        // set the local scale values to the transform's scale values
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
    }
}
