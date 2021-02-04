using System;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour{

    public GameObject IsometricGrid;
    
    private Color originalColor;
    private Color colorIncrease = new Color(1.3f, 1.3f, 1.3f); 
    private Color colorDecrease = new Color(0.5f, 0.5f, 0.5f);
    
    
    
    private bool wallSelected;
    private bool mouseButtonHoldDown;
    
    private Vector3 offset;
    
    public List<Transform> cells = new List<Transform>();
    private Vector3 spriteOffset = new Vector3(0.5f, 0.5f, 0);
    
    // Start is called before the first frame update
    void Start(){
        originalColor = this.GetComponentInChildren<SpriteRenderer>().color;

        var transforms = IsometricGrid.GetComponentsInChildren<SpriteRenderer>();
        foreach (var grid in transforms){
            cells.Add(grid.GetComponent<Transform>());
        }

        cells.Remove(this.GetComponentInChildren<SpriteRenderer>().transform);
    }
    
    private void OnMouseEnter(){
        if(!wallSelected)
            this.GetComponentInChildren<SpriteRenderer>().color *= colorIncrease;
    }

    private void OnMouseDown(){
        if(!wallSelected)
            this.GetComponentInChildren<SpriteRenderer>().color *= colorDecrease;

        mouseButtonHoldDown = true;
        wallSelected = true;
        
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void OnMouseUp(){
        var trans = snapToCell();
         transform.position = trans.position;
         if(trans != transform)
             transform.position -= transform.TransformVector(spriteOffset);
         mouseButtonHoldDown = false;
    }

    Transform snapToCell(){
        
        float minDistance = 2;
        var minTransform = transform;
        
        foreach (var cell in cells){
            //var calculatedCellPosition = transform.InverseTransformVector(cell.position) - spriteOffset;
            //var ownPosition = transform.InverseTransformVector(transform.position);
            //var calculatedCellPosition = cell.position-spriteOffset;
            var calculatedCellPosition = cell.position-spriteOffset;

            var ownPosition = transform.position;
            
            //dont cheat and set them to 0
            //instead make it use the correct z-position
            ownPosition.z = 0;
            calculatedCellPosition.z = 0;
            var currDistance = Vector3.Distance(calculatedCellPosition, ownPosition);
            Debug.Log("cell position: " + calculatedCellPosition);
            Debug.Log("own transform position: " + ownPosition);
            if(currDistance < minDistance){
                minDistance = currDistance;
                minTransform = cell;
            }
        }
        return minTransform;
    }

    private void OnMouseExit(){
        if (!mouseButtonHoldDown){
            this.GetComponentInChildren<SpriteRenderer>().color = originalColor;
            wallSelected = false;
        }
    }

    private void OnMouseDrag(){
        var curScreenPoint = Input.mousePosition;
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Debug.Log("current position: " + curPosition);
        //change the position to local direction
        transform.position = curPosition;
        //transform.localPosition = curPosition;
    }
}
