using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public TMP_Text name;
    public TMP_Text energyCost;
    
    public void Start()
    {
        name.text = card.name;
        energyCost.text = card.energyCost.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.Translate(new Vector2(0, 100));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.Translate(new Vector2(0, -100));
    }
}