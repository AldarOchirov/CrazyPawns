using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Board
{
    public class Board : MonoBehaviour
    {
        [Inject]
        private BoardConfig _config;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            transform.localScale = new Vector3(_config.CheckerBoardSize, 0.0f, _config.CheckerBoardSize);
        }
    }
}
