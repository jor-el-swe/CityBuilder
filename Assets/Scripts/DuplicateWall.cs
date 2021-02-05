using UnityEngine;

public class DuplicateWall : MonoBehaviour{
    private Vector3 startingPosition;
    private Color startingColor;
    
    // Start is called before the first frame update
    void Start(){
        startingPosition = transform.position;
        startingColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    private void OnMouseDown(){
        Debug.Log("dragging/duplication");
        if (transform.position==startingPosition){
            var clone = Instantiate(gameObject. transform.parent);
            clone.GetComponentInChildren<SpriteRenderer>().color = startingColor;
        }
    }
}
