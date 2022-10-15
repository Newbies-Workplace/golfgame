using System;
using Battle;
using UnityEngine;

namespace Board
{
    public class Fighter : MonoBehaviour
    {
        public Material material;

        private void OnEnable()
        {
            
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
