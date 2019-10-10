using System.Collections.Generic;

[System.Serializable]
public class Card
{
    public string cardName; 
    public string cardDescription;

    public List<ActionData> actions;
    
    public Card(CardData cardData)
    {
        cardName = cardData.cardName;
        cardDescription = cardData.cardDescription;

        actions = cardData.actions;
    }
}