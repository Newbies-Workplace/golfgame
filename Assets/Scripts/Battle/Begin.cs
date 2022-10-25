using System.Collections;

namespace Battle
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.Player.transform.SetParent(
                battleSystem.Board.GetTileTransform(battleSystem.Player.coordinates));
            battleSystem.Player.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(battleSystem.Player.coordinates);

            battleSystem.UiManager.PlayerEnergy.SetText(
                $"Energy: {battleSystem.Player.energy}"
            );

            battleSystem.Enemy.transform.SetParent(battleSystem.Board.GetTileTransform(battleSystem.Enemy.coordinates));
            battleSystem.Enemy.transform.position =
                battleSystem.Board.GetPositionForTileEntityFromCoordinate(battleSystem.Enemy.coordinates);

            yield return battleSystem.CardManager.GenerateStarterDeck();

            battleSystem.SetState(new PlayerTurn(battleSystem));
        }
    }
}