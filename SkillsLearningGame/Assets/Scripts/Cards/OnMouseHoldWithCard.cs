using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This is unnecessary and will be removed
[CreateAssetMenu(menuName = "Actions/On Mouse Hold With Card")]
public class OnMouseHoldWithCard : MonoBehaviour//Action
{
    /*
    public GameState playerControlState;
    public SO.GameEvent onPlayerControlState;
    public CardVariable currentCard;
    public CardType acceptedType;

    public override void Execute(float d)
    {
        bool mouseIsDown = Input.GetMouseButton(0);

        if (!mouseIsDown)
        {
            List<RaycastResult> raycastResults = Settings.GetUIObjects();

            bool droppedOnArea = false;

            for(int i = 0; i < raycastResults.Count; i++)
            {
                Area area = raycastResults[i].gameObject.GetComponentInParent<Area>();
                
                if (area != null && currentCard.value.ui.card.cardType == acceptedType)
                {
                    droppedOnArea = true;
                    area.OnDrop();
                    break;
                }
            }
            if (!droppedOnArea)
            {
                currentCard.value.ResetSize();
                currentCard.value.gameObject.SetActive(true);
            }
            else
            {
                currentCard.value = null;
            }

            Settings.gameManager.SetState(playerControlState);
            onPlayerControlState.Raise();
            return;
        }
    }*/
}

