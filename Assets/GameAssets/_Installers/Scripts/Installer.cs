using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "Installer", menuName = "Installers/Installer")]
    public class Installer : ScriptableObjectInstaller<Installer>
    {
        public override void InstallBindings()
        {
        }
    }
}
