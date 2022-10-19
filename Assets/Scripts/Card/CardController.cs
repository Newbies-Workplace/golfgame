using System.Threading;
using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    public TMP_Text name;
    public TMP_Text energyCost;

    private BattleSystem battleSystem;

    public void Start()
    {
        battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();
        name.text = card.name;
        energyCost.text = card.energyCost.ToString();
        gameObject.AddComponent(typeof(Button));
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            battleSystem.OnUseCard(card);
            Destroy(gameObject);
            
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