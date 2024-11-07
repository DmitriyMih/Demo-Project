using UnityEngine;

namespace GameSystem
{
    [System.Serializable]
    public class TileCreatorManagment
    {
        [SerializeField] private Material gridMaterial;
        [field: SerializeField] public TileItemsDictionary TilesData { get; private set; } = new();

        public void ClearTileItems()
        {
            foreach (var tileItem in TilesData.Values)
            {
                if (tileItem.Container != null)
                {
                    if (Application.isPlaying)
                        Object.Destroy(tileItem.Container);
                    else
                        Object.DestroyImmediate(tileItem.Container);
                }
            }

            TilesData.Clear();
        }

        [ContextMenu("Generate Meshes")]
        public void CreateTileGroups(ITileGrid gridSettings)
        {
            ClearTileItems();
            float tileSize = PlaceManagment.PlaceTileSize;
            Vector3 totalCenterOffset = gridSettings.GridSize.CalculateTotalCenterOffset(gridSettings.GroupsCount, gridSettings.SpacingBetweenGroups, tileSize);

            for (int gx = 0; gx < gridSettings.GroupsCount.x; gx++)
            {
                for (int gz = 0; gz < gridSettings.GroupsCount.y; gz++)
                {
                    GameObject groupContainer = new GameObject($"TileGroup_{gx}_{gz}");
                    TileItem tileItem = new TileItem() { UniqueCode = $"Group_{gx}_{gz}", MeshRenderers = new(), Container = groupContainer };

                    groupContainer.transform.SetParent(gridSettings.GetTransform());
                    groupContainer.transform.localPosition = gridSettings.GridSize.GetGroupOffset(gridSettings.SpacingBetweenGroups, gx, gz, tileSize) - totalCenterOffset;

                    CreateTilesInGroup(gridSettings, tileItem, groupContainer.transform, tileSize);
                    TilesData.Add(new Vector2Int(gx, gz), tileItem);
                }
            }
        }

        private void CreateTilesInGroup(ITileGrid gridSettings, TileItem tileItem, Transform parent, float tileSize)
        {
            Vector3 gridCenterOffset = gridSettings.GridSize.CalculateGroupOffset(tileSize);

            for (int x = 0; x < gridSettings.GridSize.x; x++)
            {
                for (int z = 0; z < gridSettings.GridSize.z; z++)
                {
                    Vector3 tilePosition = -gridCenterOffset + new Vector3(x * tileSize, 0, z * tileSize);
                    GameObject tileObject = new GameObject($"Tile_{x}_{z}");

                    tileObject.transform.SetParent(parent);
                    tileObject.transform.localPosition = tilePosition;

                    MeshFilter meshFilter = tileObject.AddComponent<MeshFilter>();
                    MeshRenderer meshRenderer = tileObject.AddComponent<MeshRenderer>();

                    meshFilter.mesh = CreateTileMesh(tileSize);

                    if (gridMaterial != null)
                        meshRenderer.material = new Material(gridMaterial);

                    tileItem.MeshRenderers.Add(meshRenderer);
                }
            }
        }

        private Mesh CreateTileMesh(float size)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices =
            {
                new Vector3(-size / 2, 0, -size / 2),
                new Vector3(size / 2, 0, -size / 2),
                new Vector3(size / 2, 0, size / 2),
                new Vector3(-size / 2, 0, size / 2)
            };

            int[] triangles = { 0, 2, 1, 0, 3, 2 };

            Vector2[] uv =
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}