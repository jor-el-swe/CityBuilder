using System.Collections.Generic;
using UnityEngine;

public class IsoGrid : MonoBehaviour
{
    public List<Transform> cells = new List<Transform>();
    public Vector3 spriteOffset = new Vector3(0.5f, 0.5f, 0);

    public void removeTransformFromGrid(Transform removeTransform){
        cells.Remove(removeTransform);
    }
    
    public Transform snapToCell(Transform snapTransform){

        var calculatedCellPosition = Vector3.zero;
        var cellPosition = snapTransform.position;
        
        float minDistance = 2;
        var minTransform = snapTransform;
        
        foreach (var cell in cells){
            calculatedCellPosition = cell.position - cell.TransformVector(spriteOffset);
            
            var currDistance = Vector3.Distance(calculatedCellPosition, cellPosition);
            
            if(currDistance < minDistance && !cell.GetComponentInParent<TileInfo>().ContainsWall){
                minDistance = currDistance;
                minTransform = cell;
            }
        }

        //we have attached a wall
        if (minTransform != snapTransform){
            minTransform.GetComponentInParent<TileInfo>().ContainsWall = true;
        }
        
        return minTransform;
    }
    
    void Start(){
        var transforms = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (var grid in transforms){
            cells.Add(grid.GetComponent<Transform>());
        }
    }
}
