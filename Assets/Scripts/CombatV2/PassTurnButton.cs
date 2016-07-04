using UnityEngine;
using System.Collections;
using S_M_D.Character;

public class PassTurnButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (Camera.main.GetComponent<CombatLogic>().monstersTurn)
            return;
        BaseCharacter c = StartCombat.Combat.NextTurn();
        Camera.main.GetComponent<CombatLogic>().timeLeft = 30.0f;
        //GameObject.Find("SpellInfo").GetComponent<Text>().text = "";

        GameObject[] arrows;
        if (GameObject.Find("redarrow") != null)
        {
            arrows = GameObject.FindGameObjectsWithTag("redArrow");
            foreach (GameObject arrowObj in arrows)
            {
                Destroy(arrowObj.gameObject);
            }
        }
        if (GameObject.Find("AttackButton") != null)
        {
            Destroy(GameObject.Find("AttackButton").gameObject);
        }
        if (c is BaseHeros)
        {
            Camera.main.GetComponent<StartCombat>().ChangeSpells((BaseHeros)c);
            HerosIni.SetMenu((BaseHeros)c);
        }
        else
            Camera.main.GetComponent<CombatLogic>().DoMonstersAction((BaseMonster)c);
    }
}
