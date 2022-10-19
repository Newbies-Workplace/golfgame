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
            if (!battleSystem.Board.CanMoveOnTile(coordinates)) yield break;
            
            battleSystem.Player.transform.SetParent(battleSystem.Board.GetTileTransform(coordinates));
            battleSystem.Player.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);
                
            battleSystem.SetState(new EnemyTurn(battleSystem));
        }

        public override IEnumerator UseCard(Card card)
        {
            Debug.Log($"PlayerTurn: Use Card: {card.name}");
            return base.UseCard(card);
        }
    }
}