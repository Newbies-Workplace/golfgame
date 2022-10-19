using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform cardList;
    public List<GameObject> cardPrefabs;
    public int numberOfCards = 5;

    public IEnumerator GenerateStarterDeck()
    {
        yield return new WaitForSeconds(.5f);
        for (var i = 0; i < numberOfCards; i++)
        {  
            Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], cardList.transform, false);
            yield return new WaitForSeconds(.5f);
        }
    }
}