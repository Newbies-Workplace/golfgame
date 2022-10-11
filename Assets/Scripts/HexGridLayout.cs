using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")] 
    public int gridSize;

    [Header("Tile Settings")] 
    public float outerSize = 1f;
    public float innerSize = 0f;
    public float height = 1f;
    public bool isFlatTopped;
    public Material material;

    private void OnEnable()
    {
        LayoutGrid();
    }
    
    private void LayoutGrid()
    {
        for (var y = 0; y < gridSize; y++)
        {
            for (var x = 0; x < gridSize; x++)
            {
                var tile = new GameObject($"Tile {x}, {y}", typeof(HexRenderer))
                {
                    transform =
                    {
                        position = GetPositionForHexFromCoordinate(new Vector2Int(x, y))
                    }
                };

                var hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.isFlatTopped = isFlatTopped;
                hexRenderer.outerSize = outerSize;
                hexRenderer.innerSize = innerSize;
                hexRenderer.height = height;
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
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped)
        {
            shouldOffset = row % 2 == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = shouldOffset ? width / 2 : 0;

            xPosition = column * horizontalDistance + offset;
            yPosition = row * verticalDistance;
        }
        else
        {
            shouldOffset = column % 2 == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;

            offset = shouldOffset ? height / 2 : 0;
            xPosition = column * horizontalDistance;
            yPosition = row * verticalDistance - offset;
        } 

        return new Vector3(xPosition, 0, yPosition);
    }
}