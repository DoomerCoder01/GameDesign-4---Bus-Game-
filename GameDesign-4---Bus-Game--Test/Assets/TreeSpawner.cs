#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public MeshFilter terrainMeshFilter; // Assign your terrain mesh filter here
    public int numberOfTrees = 100;

    void OnEnable()
    {
        Bounds bounds = terrainMeshFilter.sharedMesh.bounds;

        for (int i = 0; i < numberOfTrees; i++)
        {
            // Generate a random position within the bounds of the mesh
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 pos = new Vector3(x, 0, z);

            // Use a raycast to get the y position of the terrain mesh at this point
            RaycastHit hit;
            Debug.DrawRay(new Vector3(x, 1000, z), Vector3.down * 1000, Color.red, 100);
            if (Physics.Raycast(new Vector3(x, 200, z), Vector3.down, out hit, Mathf.Infinity, terrainMeshFilter.gameObject.layer))
            {
                pos.y = hit.point.y;
                
            }

            // Check if the position is free
            if (!Physics.CheckSphere(pos, treePrefab.transform.localScale.x))
            {
                // If the position is free, instantiate a tree
                PrefabUtility.InstantiatePrefab(treePrefab, transform);
                Debug.Log("Tree spawned at " + pos);
            }
        }
    }
}
#endif