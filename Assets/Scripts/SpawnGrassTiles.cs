using UnityEngine;

public class SpawnGrassTiles : MonoBehaviour{
    [SerializeField]private GameObject grassPrefab;
    
    public int width;
    public int height;
    
    void Awake() {
        SpawnGridCells();
    }
    
    void SpawnGridCells() {
        for (var x = 0; x < this.width; x++) {
            for (var y = 0; y < this.height; y++) {
                var tile = Instantiate(this.grassPrefab, this.transform);
                tile.transform.localPosition = new Vector3(x, y,0);
            }
        }
    }
}
