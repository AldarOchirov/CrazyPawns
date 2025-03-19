using CrazyPawns.GameAssets.Board;
using CrazyPawns.GameAssets.Cell;
using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "Installer", menuName = "Installers/Installer")]
    public class Installer : ScriptableObjectInstaller<Installer>
    {
        [SerializeField]
        private Board _board;

        [SerializeField]
        private Cell _cell;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Board>().FromComponentInNewPrefab(_board).AsSingle().NonLazy();
            Container.BindMemoryPool<Cell, CellPool>().FromComponentInNewPrefab(_cell);
        }
    }
}
