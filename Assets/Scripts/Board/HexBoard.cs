using System;
using UnityEngine;

namespace Board
{
    public class HexBoard : MonoBehaviour
    {
        public int gridSize;
        public HexTileRenderer hexTileRenderer;

        public event Action<Vector2Int> OnTilePress;

        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;

        private HexTileRenderer[,] _tiles;
        private Transform _selection;
        private Camera _currentCamera;
        private Vector2Int _currentHover = -Vector2Int.one;

        private void Awake()
        {
            LayoutGrid();
        }

        //todo refactor
        private void Update()
        {
            if (!_currentCamera)
            {
                _currentCamera = Camera.main;
                return;
            }

            var ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var info, 100, LayerMask.GetMask("Tile", "Hover")))
            {
                var hitPosition = LookupTileIndex(info.transform.gameObject.GetComponent<HexTileRenderer>());

                // Initial hover
                if (_currentHover == -Vector2Int.one)
                {
                    _currentHover = hitPosition;
                    _tiles[hitPosition.x, hitPosition.y].SetMaterial(highlightMaterial);
                }

                // Hover and change previous
                if (_currentHover != hitPosition)
                {
                    _tiles[_currentHover.x, _currentHover.y].SetMaterial(defaultMaterial);
                    _currentHover = hitPosition;
                    _tiles[hitPosition.x, hitPosition.y].SetMaterial(highlightMaterial);
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    OnTilePress?.Invoke(hitPosition);
                }
            }
            else
            {
                if (_currentHover != -Vector2Int.one)
                {
                    _tiles[_currentHover.x, _currentHover.y].SetMaterial(defaultMaterial);
                    _currentHover = -Vector2Int.one;
                }
            }
        }
        
        public Vector3 GetPositionForTileEntityFromCoordinate(Vector2Int coordinate)
        {
            var pos = GetPositionForHexFromCoordinate(coordinate);

            pos.y = 1;

            return pos;
        }
        
        private Vector2Int LookupTileIndex(HexTileRenderer hitInfo)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (_tiles[x, y] == hitInfo)
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }

            return -Vector2Int.one;
        }

        public Transform GetTileTransform(Vector2Int coordinates)
        {
            return _tiles[coordinates.x, coordinates.y].transform;
        }

        public bool CanMoveOnTile(Vector2Int coordinates)
        {
            return _tiles[coordinates.x, coordinates.y].transform.childCount == 0;
        }

        private void LayoutGrid()
        {
            _tiles = new HexTileRenderer[gridSize, gridSize];
        
            var offset = gridSize / 2 - 1;

            for (var y = 0; y < gridSize; y++)
            {
                for (var x = 0; x < gridSize; x++)
                {
                    if (x + y <= offset || x + y > gridSize + offset)
                    {
                        continue;
                    }

                    var tile = Instantiate(hexTileRenderer, GetPositionForHexFromCoordinate(new Vector2Int(x, y)), Quaternion.identity);
                    tile.name = $"Tile {x}, {y}";
                    tile.transform.parent = transform;
                    tile.GetComponent<MeshRenderer>().material = defaultMaterial;
                    _tiles[x, y] = tile;
                }
            }
        }

        private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            var column = coordinate.x;
            var row = coordinate.y;
            var size = hexTileRenderer.outerSize;

            var shouldOffset = row % 2 == 0;
            var width = Mathf.Sqrt(3) * size;
            var height = 2f * size;

            var horizontalDistance = width;
            var verticalDistance = height * (3f / 4f);

            var offset = shouldOffset
                ? -coordinate.y * width / 2
                : -coordinate.y / 2f * width;

            var xPosition = column * horizontalDistance - offset;
            var yPosition = -row * verticalDistance;

            return new Vector3(xPosition, 0, yPosition);
        }
    }
}