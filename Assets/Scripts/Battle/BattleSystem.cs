using Board;
using UnityEngine;

namespace Battle
{
    public class BattleSystem : StateMachine
    {
        [SerializeField] private Fighter player;
        [SerializeField] private Fighter enemy;
        [SerializeField] private HexBoard hexBoard;
        [SerializeField] private CardManager cardManager;
        
        public Fighter Player => player;
        public Fighter Enemy => enemy;
        public HexBoard Board => hexBoard;
        public CardManager CardManager => cardManager;

        private void Start()
        {
            SetState(new Begin(this));
        }

        private void OnEnable()
        {
            hexBoard.OnTilePress += pos =>
            {
                StartCoroutine(State.Move(pos));
            };
            cardManager.OnCardPress += card =>
            {
                if (State.GetType() == typeof(PlayerTurn))
                    StartCoroutine(State.UseCard(card));
            };
        }
    }
}