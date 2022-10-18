using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    // todo podłaczyc do battle system
    public List<GameObject> cardPrefabs;
    public int numberOfCards = 5;

    private void Start()
    {
        for (var i = 0; i < numberOfCards; i++)
        {
            Instantiate(
                cardPrefabs[Random.Range(0, cardPrefabs.Count)]
            ).transform.SetParent(
                GameObject.Find("CardList").transform, false
            );
        }
    }
}