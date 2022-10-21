using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public struct Face
    {
        public List<Vector3> Vertices { get; }
        public List<int> Triangles { get; }
        public List<Vector2> Uvs { get; }

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            Vertices = vertices;
            Triangles = triangles;
            Uvs = uvs;
        }
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class HexTileRenderer : MonoBehaviour
    {
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;
        private List<Face> _faces;

        private bool _isHighlighted = false;
        private bool _isAvailable = false;
        
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material availableMaterial;

        public float height;
        public float innerSize;
        public float outerSize;
    
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshCollider = GetComponent<MeshCollider>();

            _mesh = new Mesh
            {
                name = "Hex"
            };

            _meshFilter.mesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }

        private void OnEnable()
        {
            DrawMesh();
        }

        public void SetIsHighlighted(bool isHighlighted)
        {
            _isHighlighted = isHighlighted;
            UpdateMaterial();
        }
        
        public void SetIsAvailable(bool isAvailable)
        {
            _isAvailable = isAvailable;
            UpdateMaterial();
        }
        
        private void UpdateMaterial()
        {
            var material = defaultMaterial;
            
            if (_isHighlighted)
            {
                material = highlightMaterial;
            }
            else if (_isAvailable)
            {
                material = availableMaterial;
            }
                
            _meshRenderer.material = material;
        }

        #region Drawing

        private void DrawMesh()
        {
            DrawFaces();
            CombineFaces();
        }
        
        private void DrawFaces()
        {
            _faces = new List<Face>();
        
            // Top faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
            }
                
            // Bottom faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
            }
                
            // Outer faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
            }
                
            // Inner faces
            for (var point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point));
            }
        }
        
        private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point,
            bool reverse = false)
        {
            var pointA = GetPoint(innerRad, heightB, point);
            var pointB = GetPoint(innerRad, heightB, point < 5 ? point + 1 : 0);
            var pointC = GetPoint(outerRad, heightA, point < 5 ? point + 1 : 0);
            var pointD = GetPoint(outerRad, heightA, point);
        
            var vertices = new List<Vector3> { pointA, pointB, pointC, pointD };
            var triangles = new List<int> { 0, 1, 2, 2, 3, 0 };
            var uvs = new List<Vector2> { new(0, 0), new(1, 0), new(1, 1), new(0, 1) };
            if (reverse)
            {
                vertices.Reverse();
            }
                
            return new Face(vertices, triangles, uvs);
        }
        
        private Vector3 GetPoint(float size, float height, int index)
        {
            float angleDeg = 60 * index - 30;
            float angleRad = Mathf.PI / 180f * angleDeg;
            return new Vector3(size * Mathf.Cos(angleRad), height, size * Mathf.Sin(angleRad));
        }
            
        private void CombineFaces()
        {
            var vertices = new List<Vector3>();
            var tris = new List<int>();
            var uvs = new List<Vector2>();
        
            for (var i = 0; i < _faces.Count; i++)
            {
                vertices.AddRange(_faces[i].Vertices);
                uvs.AddRange(_faces[i].Uvs);
        
                var offset = 4 * i;
                foreach (var triangle in _faces[i].Triangles)
                {
                    tris.Add(triangle + offset);
                }
            }
        
            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = tris.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

        #endregion
    }
}