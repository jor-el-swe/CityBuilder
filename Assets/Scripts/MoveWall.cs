using UnityEngine;
public class MoveWall : MonoBehaviour{
    
    private IsoGrid _isoGrid;
    
    void Start(){
        _isoGrid = FindObjectOfType<IsoGrid>();
        _isoGrid.removeTransformFromGrid(this.GetComponentInChildren<SpriteRenderer>().transform);
    }

    private void OnMouseUp(){
        var trans = _isoGrid.snapToCell(this.transform);
        this.transform.position = trans.position;
        if(trans != this.transform)
            this.transform.position -= this.transform.TransformVector(_isoGrid.spriteOffset);
    }
    
    private void OnMouseDrag(){
        var curScreenPoint = Input.mousePosition;
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) - this.transform.TransformVector(_isoGrid.spriteOffset);
        
        //here we need to transform the mouse movements: rotate the coordinate system
        Vector3 translatedPosition  = new Vector3(curPosition.x*0.7f, -curPosition.x*0.7f,0);
        translatedPosition  += new Vector3(curPosition.y,curPosition.y ,0);
        
  
        //change the position to local direction
        //this will actually let the position "slide" along the grid
        this.transform.localPosition = translatedPosition;
    }
}
