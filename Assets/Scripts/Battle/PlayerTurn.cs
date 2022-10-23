using System.Collections;
using UnityEngine;

namespace Battle
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.Board.HighlightAvailableMoves(battleSystem.Player);

            if (battleSystem.CardManager.cardList.childCount < battleSystem.CardManager.numberOfCards)
                yield return battleSystem.CardManager.DrawCard();
            
            yield return base.Start();
        }

        public override IEnumerator Move(Vector2Int coordinates)
        {
            if (!battleSystem.Board.CanMoveToTile(battleSystem.Player, coordinates)) yield break;

            battleSystem.Player.coordinates = coordinates;
            battleSystem.Player.transform.SetParent(battleSystem.Board.GetTileTransform(coordinates));
            battleSystem.Player.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);
            
            battleSystem.Board.ClearAvailableMovesHighlight();

            battleSystem.SetState(new EnemyTurn(battleSystem));
        }

        public override IEnumerator UseCard(Card card)
        {
            Debug.Log($"PlayerTurn: Use Card: {card.cardName}");
            return base.UseCard(card);
        }
    }
}