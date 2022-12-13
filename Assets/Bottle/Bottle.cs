using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    GridManager gridManager;
    Vector2Int coordinates;
    MeshRenderer bottleMesh;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        bottleMesh = GetComponentInChildren<MeshRenderer>();
        LearnCoordinates();
    }

    void Update()
    {
        SetBottleState();
    }

    void LearnCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        coordinates = new Vector2Int(coordinates.x, coordinates.y);
    }

    void SetBottleState()
    {
        
        if(gridManager == null) {return;}

        Node node = gridManager.GetNode(coordinates);

        if(node == null) {return;}

        
        if(node.isPath)
        {
            bottleMesh.enabled = true;
        }

        else if(!node.isPath)
        {
            bottleMesh.enabled = false;
        }

    }

    

    
}
