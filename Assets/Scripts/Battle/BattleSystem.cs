﻿using Board;
using TMPro;
using UnityEngine;

namespace Battle
{
    public class BattleSystem : StateMachine
    {
        [SerializeField] private Fighter player;
        [SerializeField] private Fighter enemy;
        [SerializeField] private HexBoard hexBoard;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private TMP_Text playerEnergyText;

        public Fighter Player => player;
        public Fighter Enemy => enemy;
        public HexBoard Board => hexBoard;
        public CardManager CardManager => cardManager;
        public TMP_Text PlayerEnergy => playerEnergyText;
       
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
                StartCoroutine(State.UseCard(card));
            };
        }
    }
}