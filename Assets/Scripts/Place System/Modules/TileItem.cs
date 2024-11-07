using UnityEngine;
using System.Collections.Generic;

namespace GameSystem
{
    [System.Serializable]
    public struct TileItem
    {
        [field: SerializeField] public string UniqueCode { get; set; }
        [field: SerializeField] public GameObject Container { get; set; }
        [field: SerializeField] public List<MeshRenderer> MeshRenderers { get; set; }
    }
}