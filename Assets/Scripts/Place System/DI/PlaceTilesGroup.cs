using UnityEngine;

namespace GameSystem
{
    public partial class PlaceTilesGroup : MonoBehaviour, ITileGrid
    {
        [field: Header("Group Settings")]
        [field: SerializeField] public Vector3Int GridSize { get; private set; } = Vector3Int.one;

        [field: Space]
        [field: SerializeField] public Vector2Int GroupsCount { get; private set; } = Vector2Int.one;
        [field: SerializeField] public Vector2 SpacingBetweenGroups { get; private set; } = Vector2.one;

        [field: Header("Creator Settings")]
        [field: SerializeField] public TileCreatorManagment CreatorManagment { get; private set; }
        [field: SerializeField] public TileGizmosManagment GizmosManagment { get; private set; }
        public Transform GetTransform() => transform;

        #region Mesh Metods
        [ContextMenu("Generate Meshes")]
        public void CreateTileGroups() => CreatorManagment.CreateTileGroups(this);
        #endregion

        #region Gizmos Metods
#if UNITY_EDITOR
        private void OnDrawGizmos() => GizmosManagment.OnGizmos(this);
#endif
        #endregion
    }
}