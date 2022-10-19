using System.Collections;
using UnityEngine;

namespace Battle
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.Player.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(new Vector2Int(3, 0));
            battleSystem.Enemy.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(new Vector2Int(3, 6));

            yield return battleSystem.CardManager.GenerateStarterDeck();

            battleSystem.SetState(new PlayerTurn(battleSystem));
        }
    }
}