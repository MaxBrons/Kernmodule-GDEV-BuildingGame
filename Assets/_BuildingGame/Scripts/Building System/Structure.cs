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
            previewStructure = Instantiate(
                _structurePreviewPrefab, pos, Quaternion.identity * _structurePreviewPrefab.transform.localRotation
                );
        }

        public void DestroyPreviewStructure()
        {
            Destroy(previewStructure);
        }

        public void MoveStructure(Vector3 pos, Vector3 rot)
        {
            previewStructure.transform.position = pos;
            previewStructure.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z) * _structurePreviewPrefab.transform.localRotation;
        }

        public bool TryPlace(Vector3 pos, Vector3 rot)
        {
            Instantiate(_structurePrefab, pos, Quaternion.identity * Quaternion.Euler(rot.x, rot.y, rot.z) * _structurePreviewPrefab.transform.localRotation);

            // TODO: Return false when conditions are not legal
            return true;
        }
    }
}
