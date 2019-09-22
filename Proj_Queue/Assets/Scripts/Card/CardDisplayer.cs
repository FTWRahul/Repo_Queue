using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CardDisplayer : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private TextMeshProUGUI descriptionTmp;
    [SerializeField] private TextMeshProUGUI damageTmp;
    [SerializeField] private TextMeshProUGUI energyTmp;

    public void DisplayCard(Card card)
    {
        nameTmp.text = card.cardName;
        descriptionTmp.text = card.cardDescription;
        damageTmp.text = card.damage.ToString();
        energyTmp.text = card.energyCost.ToString();
    }
}
