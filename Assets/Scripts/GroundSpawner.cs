using UnityEngine;

public class GroundSpawner : MonoBehaviour {

    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile (bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }

    public void SpawnTile ()
    {
        SpawnTile(false); // Default to false if no parameter is provided
    }

    private void Start () {
        for (int i = 0; i < 15; i++) {
            SpawnTile(); // Call the parameterless version
        }
    }
}
