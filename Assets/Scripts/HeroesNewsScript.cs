using UnityEngine;
using System.Collections;
using Assets.ParticularName;
using UnityEngine.UI;
using S_M_D.Character;

public class HeroesNewsScript : MonoBehaviour
{
    
    private GameObject pickObject;
    private BaseHeros actualHero;
    private Vector2 touchOffset;
    private Vector2 oldPosition;

    void Update()
    {
        if (HasInput)
        {
            PickUp();
        }
        if(pickObject != null)
        {
            actualHero = GameScript.GameContext.PlayerInfo.MyHeros[GetIndice(pickObject.name)];
            //Debug.Log("name : " + pickObject.name + "; indice : " + GetIndice(pickObject.name));
            //AFFICHAGE DE LA GUI
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

    private int GetIndice(string nameObject)
    {
        return int.Parse("" + nameObject[nameObject.Length - 1]);
    }

    private void PickUp()
    {
        var inputPosition = CurrentTouchPosition;

        RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
        if (touches.Length > 0)
        {
            var hit = touches[0];
            if (hit.transform != null && hit.transform.gameObject.tag == TagName.pHero.ToString())
            {
                pickObject = hit.transform.gameObject;
                touchOffset = (Vector2)hit.transform.position - inputPosition;
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
}
