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
        
        offset = gameObject.transform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void OnMouseUp(){
        var trans = snapToCell();
         transform.position = trans.position;
         Debug.Log("calculated cell position: " + trans.parent.localPosition);
         Debug.Log("new transform position: " + transform.localPosition);
         if(trans != transform)
             transform.position -= transform.TransformVector(spriteOffset);
         mouseButtonHoldDown = false;
    }

    Transform snapToCell(){

        var calculatedCellPosition = Vector3.zero;
        var ownPosition = transform.position;
        
        float minDistance = 2;
        var minTransform = transform;
        
        foreach (var cell in cells){
            calculatedCellPosition = cell.position-cell.TransformVector(spriteOffset);
            
            var currDistance = Vector3.Distance(calculatedCellPosition, ownPosition);
            
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
        
        //here we need to transform the mouse movements: rotate the coordinate system based on the rotation angle
        Vector3 translatedPosition = 
            new Vector3(
                curPosition.x * Mathf.Cos(45) + curPosition.y*Mathf.Sin(45), 
                -curPosition.x * Mathf.Sin(45) + curPosition.y*Mathf.Cos(45),
                0);

        Debug.Log("translated position: " + translatedPosition);
        
        //change the position to local direction
        //this will actually let the position "slide" along the grid
        transform.localPosition = curPosition;
        //transform.localPosition = translatedPosition;
    }
}
