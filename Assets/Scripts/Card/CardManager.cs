using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public Transform cardList;
    public List<CardController> cardPrefabs;
    public int numberOfCards = 5;
    
    public event Action<Card> OnCardPress;
    
    public IEnumerator GenerateStarterDeck()
    {
        yield return new WaitForSeconds(.5f);
        for (var i = 0; i < numberOfCards; i++)
        {  
            var cardController = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], cardList.transform, false);
            cardController.OnCardPress += () =>
            {
                OnCardPress?.Invoke(cardController.card);
                Destroy(cardController.gameObject);
            };
            
            yield return new WaitForSeconds(.1f);
        }
    }
}