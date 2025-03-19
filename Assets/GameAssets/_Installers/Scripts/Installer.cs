using CrazyPawns.GameAssets.Board;
using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "Installer", menuName = "Installers/Installer")]
    public class Installer : ScriptableObjectInstaller<Installer>
    {
        [SerializeField]
        private Board _board;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Board>().FromComponentInNewPrefab(_board).AsSingle().NonLazy();
        }
    }
}
