using System.Collections;
using UnityEngine;

namespace Battle
{
    public abstract class State
    {
        protected readonly BattleSystem battleSystem;

        protected State(BattleSystem battleSystem)
        {
            this.battleSystem = battleSystem;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }
        
        public virtual IEnumerator Move(Vector2Int coordinates)
        {
            yield break;
        }  
        
        public virtual IEnumerator UseCard(Card card)
        {
            yield break;
        }
    }
}