using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene( 2 );
        BoardManager.Map = BaseCombat.Map;
        BoardManager.Gtx = BaseCombat.Gtx;
        BoardManager.hero = BaseCombat.Heros;
    }
}
