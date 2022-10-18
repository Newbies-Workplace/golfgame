using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public List<GameObject> cardPrefabs;
    public int numberOfCards = 5;

    private void Start()
    {
        StartCoroutine(GenerateStarterDeck());
    }

    private IEnumerator GenerateStarterDeck()
    {
        yield return new WaitForSeconds(.5f);
        for (var i = 0; i < numberOfCards; i++)
        {            
            Instantiate(
                cardPrefabs[Random.Range(0, cardPrefabs.Count)]
            ).transform.SetParent(
                GameObject.Find("CardList").transform, false
            );
            yield return new WaitForSeconds(.5f);
        }
    }
}