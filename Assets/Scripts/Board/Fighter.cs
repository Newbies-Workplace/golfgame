using UnityEngine;

namespace Board
{
    public class Fighter : MonoBehaviour
    {
        public Material material;
        public Vector2Int coordinates;
        public int range = 1;
        public int energy = 5;
        
        private void OnEnable()
        {
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
