using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TMP_Text name;
    public TMP_Text energyCost;

    public void Start()
    {
        name.text = card.name;
        energyCost.text = card.energyCost.ToString();
    }

}
