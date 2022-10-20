using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public TMP_Text cardName;
    public TMP_Text energyCost;

    public event Action OnCardPress;

    public void Start()
    {
        cardName.text = card.cardName;
        energyCost.text = card.energyCost.ToString();
        gameObject.AddComponent(typeof(Button));
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnCardPress?.Invoke();
        });
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