using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene( 2 );
        BoardManager.Map = StartCombat.Map;
        BoardManager.Gtx = StartCombat.Gtx;
        BoardManager.hero = StartCombat.Heros;
    }
}
