using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        void Start()
        {
            // Code Here
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {//如果按下J键，则会获取当前位置的单元格位置，并从Tilemap中获取该位置的瓷砖，然后将该位置的瓷砖设置为null。
                var cellPosition = Grid.WorldToCell(transform.position);
                var tile = Tilemap.GetTile(cellPosition);
                Tilemap.SetTile(cellPosition, null);
            }
        }
    }
}
