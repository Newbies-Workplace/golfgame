using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")] 
    public int gridSize;

    [Header("Tile Settings")] 
    public float outerSize = 1f;
    public float innerSize = 0f;
    public float tileHeight = 1f;
    public Material material;

    private GameObject[,] _tiles;
    private Camera _currentCamera;
    private Vector2Int _currentHover = -Vector2Int.one;

    private void OnEnable()
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

        RaycastHit info;
        var ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile", "Hover")))
        {
            var hitPosition = LookupTileIndex(info.transform.gameObject);

            // Initial hover
            if (_currentHover == -Vector2Int.one)
            {
                _currentHover = hitPosition;
                _tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                
            }

            // Hover and change previous
            if (_currentHover != hitPosition)
            {
                _tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                _currentHover = hitPosition;
                _tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        }
        else
        {
            if (_currentHover != -Vector2Int.one)
            {
                _tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                _currentHover = -Vector2Int.one;
            }
        }
    }

    private void LayoutGrid()
    {
        _tiles = new GameObject[gridSize, gridSize];
        
        var offset = gridSize / 2 - 1;

        for (var y = 0; y < gridSize; y++)
        {
            for (var x = 0; x < gridSize; x++)
            {
                if (x + y <= offset || x + y > gridSize + offset)
                {
                    continue;
                }

                var tile = new GameObject($"Tile {x}, {y}", typeof(HexRenderer))
                {
                    transform =
                    {
                        position = GetPositionForHexFromCoordinate(new Vector2Int(x, y))
                    }
                };

                var hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.outerSize = outerSize;
                hexRenderer.innerSize = innerSize;
                hexRenderer.height = tileHeight;
                hexRenderer.SetMaterial(material);
                hexRenderer.DrawMesh();

                tile.layer = LayerMask.NameToLayer("Tile");
                tile.AddComponent<MeshCollider>();
                tile.transform.parent = transform;
                _tiles[x, y] = tile;
            }
        }
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        var column = coordinate.x;
        var row = coordinate.y;
        var size = outerSize;

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

    private Vector2Int LookupTileIndex(GameObject hitInfo)
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
}