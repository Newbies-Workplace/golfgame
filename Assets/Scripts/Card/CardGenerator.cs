using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    //todo dodać gerenowanie na dole ekranu
    // todo podłaczyc do battle system
    public List<GameObject> cardPrefabs;
    public int numberOfCards = 10;

    private void Start()
    {
        for (var i = 0; i < numberOfCards; i++)
        {
            Instantiate(
                cardPrefabs[Random.Range(0, cardPrefabs.Count)], 
                new Vector3(Screen.width  - 150 , Screen.height - 250 - (i + 1) * 35),
                Quaternion.identity
            ).transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
}