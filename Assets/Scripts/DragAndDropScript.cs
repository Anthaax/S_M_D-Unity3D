using UnityEngine;
using System.Collections;
using Assets.ParticularName;
using UnityEngine.UI;

public class DragAndDropScript : MonoBehaviour {

    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;
    private Vector2 oldPosition;

    void Update()
    {
        if (HasInput)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;

        if (draggingItem)
        {
            //draggedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            draggedObject.transform.position = inputPosition + touchOffset;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.gameObject.tag == TagName.pHero.ToString())
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    oldPosition = draggedObject.transform.position;
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                }
            }
        }
    }

    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }

    void DropItem()
    {
        draggingItem = false;
        Debug.Log("Left !!!");
        //draggedObject.transform.localScale = new Vector3(1f, 1f, 1f);
        draggedObject.transform.position = oldPosition;
    }
}
