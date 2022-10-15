using System.Collections;
using UnityEngine;

namespace Battle
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Move(Vector2Int coordinates)
        {
            BattleSystem.Enemy.transform.position =
                BattleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);
            
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
            yield break;
        }
    }
}