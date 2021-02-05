using UnityEngine;
public class HighlightDarkenCell : MonoBehaviour
{
    private Color originalColor;
    private Color colorIncrease = new Color(1.3f, 1.3f, 1.3f); 
    private Color colorDecrease = new Color(0.5f, 0.5f, 0.5f);
    
    private bool wallSelected;
    private bool mouseButtonHoldDown;
    
    void Start()
    {
        originalColor = this.GetComponentInChildren<SpriteRenderer>().color;
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
    }
    
    private void OnMouseUp(){
        mouseButtonHoldDown = false;
    }
    
    private void OnMouseExit(){
        if (!mouseButtonHoldDown){
            this.GetComponentInChildren<SpriteRenderer>().color = originalColor;
            wallSelected = false;
        }
    }
}
