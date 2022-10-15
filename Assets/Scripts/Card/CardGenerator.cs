using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public List<GameObject> cardPrefabs;
    public int numberOfCards = 10;

    private void Start()
    {
        for (var i = 0; i < numberOfCards; i++)
        {
            Instantiate(
                cardPrefabs[Random.Range(0, 2)], 
                new Vector3(Screen.width  - 150 , Screen.height - 250 - (i + 1) * 35),
                Quaternion.identity
            ).transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
}