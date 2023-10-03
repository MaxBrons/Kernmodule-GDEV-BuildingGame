using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    [CreateAssetMenu()]
    public class Structure : ScriptableObject
    {
        [SerializeField] private GameObject _structurePreviewPrefab;
        [SerializeField] private GameObject _structurePrefab;

        public SnappingPoint[] snappingPoints;

        private GameObject previewStructure;

        public void SpawnPreviewStructure(Vector3 pos)
        {
            previewStructure = Instantiate(_structurePreviewPrefab, pos, Quaternion.identity);
        }

        public void MoveStructure(Vector3 pos)
        {
            previewStructure.transform.position = pos;
        }

        public bool TryPlace(Vector3 pos)
        {
            Instantiate(_structurePrefab, pos, Quaternion.identity);
            return true;
        }
    }
}
