using UnityEngine;

[CreateAssetMenu()]
public class Structure : ScriptableObject
{
    [SerializeField] private GameObject structurePrefab;

    private GameObject previewStructure;
    
    public void SpawnPreviewStructure(Vector3 pos)
    {
        Debug.Log("Spawned preview Structure");
        previewStructure = Instantiate(structurePrefab, pos, Quaternion.identity);
    }

    public void MoveStructure(Vector3 pos)
    {
        previewStructure.transform.position = pos;
    }

    public bool TryPlace()
    {
        return false;
    }
}
