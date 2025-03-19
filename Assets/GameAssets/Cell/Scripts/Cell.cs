using UnityEngine;

namespace CrazyPawns.GameAssets.Cell
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void Reinitialize(CellConfig config)
        {
            _meshRenderer.material = config.Material;
        }
    }
}
