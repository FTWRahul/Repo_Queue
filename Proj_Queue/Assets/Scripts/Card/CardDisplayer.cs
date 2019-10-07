using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CardDisplayer : MonoBehaviour
{
    public Card CardInfo { get; private set; }
    
    private string _name;
    private string _description;
    
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private TextMeshProUGUI descriptionTmp;

    public string Name
    {
        get => _name;
        private set
        {
            _name = value;
            nameTmp.text = value;
        }
    }

    private string Description
    {
        set
        {
            _description = value;
            descriptionTmp.text = value;
        }
    }
    
    public void Init(Card card)
    {
        CardInfo = card;
        Name = card.cardName;
        Description = card.cardDescription;
    }
}
