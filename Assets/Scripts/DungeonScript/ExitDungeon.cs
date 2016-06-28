using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitDungeon : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene( 1 );
            BaseCombat.Gtx.DungeonManager.EndOfTheDuengon( );
            Start.Gtx = BaseCombat.Gtx;
        }
	}
}
