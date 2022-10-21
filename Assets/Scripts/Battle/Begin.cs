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
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(battleSystem.Player.coordinates);
            battleSystem.Enemy.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(battleSystem.Enemy.coordinates);

            yield return battleSystem.CardManager.GenerateStarterDeck();

            battleSystem.SetState(new PlayerTurn(battleSystem));
        }
    }
}