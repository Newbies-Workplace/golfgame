using System.Collections;
using UnityEngine;

namespace Battle
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.Board.HighlightAvailableMoves(battleSystem.Enemy);
            return base.Start();
        }

        public override IEnumerator Move(Vector2Int coordinates)
        {
            if (!battleSystem.Board.CanMoveToTile(battleSystem.Enemy, coordinates)) yield break;
            
            battleSystem.Enemy.coordinates = coordinates;
            battleSystem.Enemy.transform.SetParent(battleSystem.Board.GetTileTransform(coordinates));
            battleSystem.Enemy.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(coordinates);
                
            battleSystem.Board.ClearAvailableMovesHighlight();

            battleSystem.SetState(new PlayerTurn(battleSystem));
        }
    }
}