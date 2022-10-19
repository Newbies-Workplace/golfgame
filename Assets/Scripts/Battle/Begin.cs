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
            yield return BattleSystem.CardManager.GenerateStarterDeck();
            
            BattleSystem.Player.transform.position =
                BattleSystem.Board.GetPositionForTileEntityFromCoordinate(new Vector2Int(3, 0));

            BattleSystem.Enemy.transform.position =
                BattleSystem.Board.GetPositionForTileEntityFromCoordinate(new Vector2Int(3, 6));

            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}