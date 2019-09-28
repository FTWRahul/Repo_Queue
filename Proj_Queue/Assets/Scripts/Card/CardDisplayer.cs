using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CardDisplayer : MonoBehaviour
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameTmp.text = value;
        }
    }
    [SerializeField] private TextMeshProUGUI nameTmp;

    private string _description;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            descriptionTmp.text = value;
        }
    }
    [SerializeField] private TextMeshProUGUI descriptionTmp;

    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            damageTmp.text = value.ToString();
        }
    }
    [SerializeField] private TextMeshProUGUI damageTmp;
    
    private int _energy;
    public int Energy
    {
        get { return _energy; }
        set
        {
            _energy = value;
            energyTmp.text = value.ToString();
        }
    }
    [SerializeField] private TextMeshProUGUI energyTmp;
    
}
