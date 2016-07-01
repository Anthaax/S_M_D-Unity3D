using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpellInformation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown( 0 ))
        {
            EventSystem es = EventSystem.current;
            if (es == null)
            {
                Debug.Log( "Es est null" );
                return;
            }
            GameObject currentSel = es.currentSelectedGameObject;
            if (currentSel == null)
                return;
            Debug.Log( currentSel.name );
        }
    }

    public void ShowSpellInformation()
    {
        Debug.Log( "zqkdmlzejfiozndpoimsrjgmzlqsdjreofdihtsdmo!lgjfdo" );
    }
}
