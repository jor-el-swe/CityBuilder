
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Wall : MonoBehaviour{
    
    private Color originalColor;
    private Color colorIncrease = new Color(1.3f, 1.3f, 1.3f); 
    private Color colorDecrease = new Color(0.5f, 0.5f, 0.5f); 
    
    
    
    private Vector3 lastMousePosition;
    private bool buttonHoldDown;
    private bool wallSelected;
    
    
    // Start is called before the first frame update
    void Start(){
        originalColor = this.GetComponentInChildren<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter(){
        if(!buttonHoldDown)
            this.GetComponentInChildren<SpriteRenderer>().color *= colorIncrease;
    }

    private void OnMouseDown(){
        if(!wallSelected)
            this.GetComponentInChildren<SpriteRenderer>().color *= colorDecrease;
        
        lastMousePosition = Input.mousePosition;
        buttonHoldDown = true;
        wallSelected = true;
    }

    private void OnMouseUp(){
        buttonHoldDown = false;
    }

    private void OnMouseExit(){
        if (!buttonHoldDown){
            this.GetComponentInChildren<SpriteRenderer>().color = originalColor;
            wallSelected = false;
        }
    }

    private void OnMouseDrag(){
        var distance = Input.mousePosition - lastMousePosition;
        transform.position = distance/20;
    }
}
