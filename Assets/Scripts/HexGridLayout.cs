using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")] public int gridSize;

    [Header("Tile Settings")] public float outerSize = 1f;
    public float innerSize = 0f;
    public float tileHeight = 1f;
    public Material material;

    private void OnEnable()
    {
        LayoutGrid();
    }

    private void LayoutGrid()
    {
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

                tile.transform.parent = transform;
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
}