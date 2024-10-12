using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShipModulePanel : MonoBehaviour
    {
        private PlayerShip _playerShip;
        private GridLayoutGroup _gridLayoutGroup;
        
        private int _shipModules;
        private int _moduleDisplaySize;
        void Start()
        {
            _playerShip = GameManager.CurrentPlayerShip;
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            
            _shipModules = 10; // todo do ships have a max amount of modules?
        }

        void Update()
        {
            _moduleDisplaySize = Screen.height / 10;
            _gridLayoutGroup.cellSize = new Vector2(_moduleDisplaySize, _moduleDisplaySize);
        }
    }
}
