using System.Collections;
using UnityEngine;

namespace Battle
{
    public abstract class State
    {
        protected BattleSystem BattleSystem;

        public State(BattleSystem battleSystem)
        {
            BattleSystem = battleSystem;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }
        
        public virtual IEnumerator Move(Vector2Int coordinates)
        {
            yield break;
        }
    }
}