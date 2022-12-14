using System;
using UnityEngine;

namespace Board
{
    public class HexBoard : MonoBehaviour
    {
        public int gridSize;
        public HexTileRenderer hexTileRenderer;

        public event Action<Vector2Int> OnTilePress;
        
        private HexTileRenderer[,] _tiles;
        private Transform _selection;
        private Camera _currentCamera;
        private Vector2Int? _currentHover;

        private void Awake()
        {
            LayoutGrid();
        }

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
                if (hitPosition is null) return;
                
                HighlightTile(hitPosition.Value);
                
                if (Input.GetMouseButtonDown(0))
                {
                    OnTilePress?.Invoke(hitPosition.Value);
                }
            }
            else
            {
                ClearHighlightedTile();
            }
        }
        
        public Vector3 GetPositionForTileEntityFromCoordinate(Vector2Int coordinate)
        {
            var pos = GetPositionForHexFromCoordinate(coordinate);

            pos.y = 1;

            return pos;
        }

        private void HighlightTile(Vector2Int hitPosition)
        {
            if (_currentHover == hitPosition) return;
            
            ClearHighlightedTile();
            _currentHover = hitPosition;
            _tiles[hitPosition.x, hitPosition.y].SetIsHighlighted(true);
        }

        private void ClearHighlightedTile()
        {
            if (_currentHover == null) return;
            
            _tiles[_currentHover.Value.x, _currentHover.Value.y].SetIsHighlighted(false);
            _currentHover = null;
        }
        
        private Vector2Int? LookupTileIndex(HexTileRenderer hitInfo)
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

            return null;
        }

        public Transform GetTileTransform(Vector2Int coordinates)
        {
            return _tiles[coordinates.x, coordinates.y].transform;
        }

        public bool CanMoveToTile(Fighter fighter, Vector2Int to)
        {
            return HexAlgorithms.CanMoveToHex(fighter.coordinates, to, fighter.range, pos => !CanStepOnTile(pos));
        }
        
        //https://www.redblobgames.com/grids/hexagons/#range
        public void HighlightAvailableMoves(Fighter player)
        {
            var tilesWithinRange = HexAlgorithms.GetMovableHexes(
                player.coordinates,
                player.range,
                pos => !CanStepOnTile(pos)
            );

            foreach (var coordinate in tilesWithinRange)
            {
                try //todo check instead of try
                {
                    _tiles[coordinate.x, coordinate.y].SetIsAvailable(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void ClearAvailableMovesHighlight()
        {
            foreach (var tile in _tiles)
            {
                if (tile != null)
                {
                    tile.SetIsAvailable(false);
                }
            }
        }

        private bool CanStepOnTile(Vector2Int pos)
        {
            try //todo check instead of try
            {
                return _tiles[pos.x, pos.y].transform.childCount == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private void LayoutGrid()
        {
            _tiles = new HexTileRenderer[gridSize, gridSize];
        
            var offset = gridSize / 2 - 1;

            for (var r = 0; r < gridSize; r++)
            {
                for (var q = 0; q < gridSize; q++)
                {
                    if (q + r <= offset || q + r > gridSize + offset)
                    {
                        continue;
                    }

                    var tile = Instantiate(hexTileRenderer, GetPositionForHexFromCoordinate(new Vector2Int(q, r)), Quaternion.identity);
                    tile.name = $"Tile {q}, {r}";
                    tile.transform.parent = transform;
                    _tiles[q, r] = tile;
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