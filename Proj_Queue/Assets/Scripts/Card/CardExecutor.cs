using UnityEngine;

public class CardExecutor : MonoBehaviour
{
    private Card CardInfo { get; set; }
    [SerializeField] private CardDisplayer cardDisplayer;
    [SerializeField] private Draggable draggable;

    private void Start()
    {
        //Subscribing to the drop on schedule deck method
        draggable.CardScheduledEvent += ScheduleTheCard;
    }

    public void Init(Card card)
    {
        CardInfo = card;
        cardDisplayer.Init(CardInfo);
        draggable.enabled = true;
    }

    private void ReceiveSelectedCell(Vector2Int cellPos)
    {
        foreach (var action in CardInfo.actions)
        {
            action.originCell = cellPos;
        }

        //Unsubscribing from ReceiveSelectedCellEvent 
        Board.BoardInstance.gameManager.ReceiveSelectedCellEvent -= ReceiveSelectedCell;
        //Calling method when card was scheduled
        Board.BoardInstance.gameManager.OnCardReceiveCellEvent();
    }

    private void ScheduleTheCard()
    {
        
        Board.BoardInstance.gameManager.OnCardDropEvent();
        Board.BoardInstance.gameManager.ReceiveSelectedCellEvent += ReceiveSelectedCell;
        draggable.enabled = false;
        
        foreach (var action in CardInfo.actions)
        {
            action.DisplayPossiblePattern(Board.BoardInstance.GetPlayerPosition(Board.BoardInstance.CurrentPlayer));
        }
    }
}