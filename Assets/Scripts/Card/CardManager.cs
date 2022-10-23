using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public Transform cardList;
    public List<CardController> cardPrefabs;
    public int numberOfCards = 6;
    private readonly List<CardController> _cards = new();

    public event Action<Card> OnCardPress;

    public IEnumerator GenerateStarterDeck()
    {
        yield return new WaitForSeconds(.5f);
        for (var i = 0; i < numberOfCards; i++)
        {
            StartCoroutine(DrawCard());
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator DrawCard()
    {
        var cardController = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], cardList.transform, false);
        _cards.Add(cardController);

        cardController.OnCardPress += () => { OnCardPress?.Invoke(cardController.card); };
        yield break;
    }

    public void DestroyCard(Card card)
    {
        var foundCard = _cards.Find(match => match.card == card);
        _cards.Remove(foundCard);
        Destroy(foundCard.gameObject);
    }
}