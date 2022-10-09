using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GameObject[,] _tiles;
    
    private void Awake()
    {
        GenerateBoard(1, 8);
    }
    

    private void GenerateBoard(float tileSize, int tilesNumber)
    {
        _tiles = new GameObject[tilesNumber, tilesNumber];

        for (var x = 0; x < tilesNumber; x++)
        {
            for (var y = 0; y < tilesNumber; y++)
            {
                _tiles[x, y] = GenerateTile(tileSize, x, y);
            }
        }
    }

    private GameObject GenerateTile(float tileSize, int x, int y)
    {
        var tile = new GameObject($"X:{x}, Y:{y}");
        tile.transform.parent = transform;

        var mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh;
        tile.AddComponent<MeshRenderer>();

        var vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 0, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0, (y + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0, y * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0, (y + 1) * tileSize);

        var tris = new[] { 0, 1, 2, 1, 3, 2 };
        mesh.vertices = vertices;
        mesh.triangles = tris;

        tile.AddComponent<BoxCollider>();
        
        // todo finished at https://youtu.be/FtGy7J8XD90?t=853
        
        return tile;
    }
}
