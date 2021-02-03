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

    private Vector3 screenPoint;
    private Vector3 offset;
    public List<Collider2D> colliders2D = new List<Collider2D>();

    
    
    // Start is called before the first frame update
    void Start(){
        originalColor = this.GetComponentInChildren<SpriteRenderer>().color;
        var colliders = IsometricGrid.GetComponentsInChildren<Collider2D>();

        foreach (var cell in colliders){
            colliders2D.Add(cell);
        }
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
        
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }

    private void OnMouseUp(){
        mouseButtonHoldDown = false;
        
        foreach (var cell in colliders2D){
            if (this.GetComponent<Collider2D>().bounds.Intersects(cell.bounds)){
                Debug.Log("touched cell!");
            }
        }
    }

    private void OnMouseExit(){
        if (!mouseButtonHoldDown){
            this.GetComponentInChildren<SpriteRenderer>().color = originalColor;
            wallSelected = false;
        }
    }

    private void OnMouseDrag(){
        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //Debug.Log("current position: " + curPosition);
        transform.position = curPosition;
    }
}
