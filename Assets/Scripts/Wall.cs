using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour{

    public GameObject IsometricGrid;
    
    public List<Transform> cells = new List<Transform>();
    private Vector3 spriteOffset = new Vector3(0.5f, 0.5f, 0);
    
    void Start(){

        var transforms = IsometricGrid.GetComponentsInChildren<SpriteRenderer>();
        foreach (var grid in transforms){
            cells.Add(grid.GetComponent<Transform>());
        }

        cells.Remove(this.GetComponentInChildren<SpriteRenderer>().transform);
    }
    

    private void OnMouseUp(){
        var trans = snapToCell();
         transform.position = trans.position;
         Debug.Log("calculated cell position: " + trans.parent.localPosition);
         Debug.Log("new transform position: " + transform.localPosition);
         if(trans != transform)
             transform.position -= transform.TransformVector(spriteOffset);
    }

    Transform snapToCell(){

        var calculatedCellPosition = Vector3.zero;
        var ownPosition = transform.position;
        
        float minDistance = 2;
        var minTransform = transform;
        
        foreach (var cell in cells){
            calculatedCellPosition = cell.position - cell.TransformVector(spriteOffset);
            
            var currDistance = Vector3.Distance(calculatedCellPosition, ownPosition);
            
            if(currDistance < minDistance){
                minDistance = currDistance;
                minTransform = cell;
            }
        }
        return minTransform;
    }
    
    private void OnMouseDrag(){
        var curScreenPoint = Input.mousePosition;
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) - transform.TransformVector(spriteOffset);
        
        //here we need to transform the mouse movements: rotate the coordinate system
        Vector3 translatedPosition  = new Vector3(curPosition.x*0.7f, -curPosition.x*0.7f,0);
        translatedPosition  += new Vector3(curPosition.y,curPosition.y ,0);
        
        Debug.Log("translated position: " + translatedPosition);
        
        //change the position to local direction
        //this will actually let the position "slide" along the grid
        transform.localPosition = translatedPosition;
    }
}
