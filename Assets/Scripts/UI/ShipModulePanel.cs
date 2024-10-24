using Managers;
using Ships;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShipModulePanel : MonoBehaviour
    {
        private PlayerShip _playerShip;
        private GridLayoutGroup _gridLayoutGroup;
        private GameObject _moduleIcon;
        
        private int _shipModules;
        private int _moduleDisplaySize;
        void Start()
        {
            _playerShip = GameManager.CurrentPlayerShip;
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _moduleIcon = GameObject.Find("ShipModule");
            _moduleIcon.SetActive(true);
            
            // Instanciate enough module icons
            _shipModules = 10; // DONE do ships have a max amount of modules?
            for (var i = 1; i < _shipModules; i++)
            {
                var newModule = Instantiate(_moduleIcon, transform);
                newModule.transform.SetParent(_gridLayoutGroup.transform);
            }
        }

        void Update()
        {
            _moduleDisplaySize = Screen.height / 10;
            _gridLayoutGroup.cellSize = new Vector2(_moduleDisplaySize, _moduleDisplaySize);
        }
    }
}
