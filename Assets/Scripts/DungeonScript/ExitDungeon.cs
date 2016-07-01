using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ExitDungeon : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            EventSystem es = EventSystem.current;
            if (es == null)
                return;
            GameObject currentSel = es.currentSelectedGameObject;
            if (currentSel == null)
                return;
            Debug.Log(currentSel.name);
            if (currentSel.name == "ExitDungeon")
            {
                SceneManager.LoadScene(1);
                BaseCombat.Gtx.DungeonManager.EndOfTheDuengon();
                Start.Gtx = BaseCombat.Gtx;
                Array.Clear( SetProfil.HerosAdventure, 0, SetProfil.HerosAdventure.Length );

            }
        }
	}
}
