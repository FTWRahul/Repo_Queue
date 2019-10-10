using TMPro;
using UnityEngine;

public class CardDisplayer : MonoBehaviour
{
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
        Name = card.cardName;
        Description = card.cardDescription;
    }
}
