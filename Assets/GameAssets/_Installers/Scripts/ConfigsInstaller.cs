using CrazyPawns.GameAssets.Board;
using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField]
        private BoardConfig _boardConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoardConfig>().FromScriptableObject(_boardConfig).AsSingle().NonLazy();
        }
    }
}
