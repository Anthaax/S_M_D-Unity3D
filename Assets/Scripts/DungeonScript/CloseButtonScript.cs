using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CloseButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            EventSystem es = EventSystem.current;
            if ( es == null )
                return;
            GameObject currentSel = es.currentSelectedGameObject;
            if ( currentSel == null )
                return;
            Debug.Log( currentSel.name );
            if (currentSel.name == "CloseButton" )
            {
              BoardManager boardMan = GameObject.FindObjectOfType( typeof( BoardManager ) ) as BoardManager;
              boardMan.isActive = true; 
            }
        }
    }


}
