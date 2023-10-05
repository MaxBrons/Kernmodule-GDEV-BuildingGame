using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    [CreateAssetMenu()]
    public class Structure : ScriptableObject
    {
        [SerializeField] private GameObject _structurePreviewPrefab;
        [SerializeField] private GameObject _structurePrefab;

        //Snapping points for snapping new buildings on existing structures
        public Vector3[] foundationSnappingPoints;
        public Vector3[] wallSnappingPoints;

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

        public void MoveStructure(Vector3 pos)
        {
            previewStructure.transform.position = pos;
        }

        public bool TryPlace(Vector3 pos)
        {
            Instantiate(_structurePrefab, pos, Quaternion.identity * _structurePrefab.transform.localRotation);

            // TODO: Return false when conditions are not legal
            return true;
        }
    }
}
