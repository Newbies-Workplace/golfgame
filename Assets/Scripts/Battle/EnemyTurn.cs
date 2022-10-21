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
            if (!battleSystem.Board.CanMoveOnTile(coordinates)) yield break;
            
            battleSystem.Enemy.transform.SetParent(battleSystem.Board.GetTileTransform(coordinates));
            battleSystem.Enemy.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);
                
            battleSystem.SetState(new PlayerTurn(battleSystem));
        }
    }
}