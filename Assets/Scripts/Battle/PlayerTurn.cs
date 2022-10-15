using System.Collections;
using UnityEngine;

namespace Battle
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Move(Vector2Int coordinates)
        {
            BattleSystem.Player.transform.position =
                BattleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);

            BattleSystem.SetState(new EnemyTurn(BattleSystem));
            yield break;
        }
    }
}