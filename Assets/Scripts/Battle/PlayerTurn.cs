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

            if (battleSystem.GetPreviousState() is not Begin)
                battleSystem.PlayerEnergy.SetText($"Energy: {battleSystem.Player.energy += 1}");


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
            if (battleSystem.Player.energy - card.energyCost >= 0)
            {
                Debug.Log($"PlayerTurn. Use Card: {card.cardName}");
                
                battleSystem.PlayerEnergy.SetText($"Energy: {battleSystem.Player.energy -= card.energyCost}");
                battleSystem.CardManager.DestroyCard(card);
            }
            else
            {
                Debug.Log("You can't use the card");
            }

            return base.UseCard(card);
        }
    }
}