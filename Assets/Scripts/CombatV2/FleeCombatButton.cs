using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FleeCombatButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (Camera.main.GetComponent<CombatLogic>().monstersTurn)
        {
            Debug.Log("Can't flee on monster's turn.");
            return;
        }
        Debug.Log("Leaving the fight.");
        SceneManager.LoadScene(2);
        BoardManager.Map = BaseCombat.Map;
        BoardManager.Gtx = BaseCombat.Gtx;
        BoardManager.hero = BaseCombat.Heros;
    }
}
