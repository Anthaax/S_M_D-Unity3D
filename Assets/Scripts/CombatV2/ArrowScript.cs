using UnityEngine;
using System.Collections;
using S_M_D.Spell;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour {

    public bool Selected;
    public Spells AssociatedSpell;
    public GameObject AttackPrefab;
    public int MonsterPosition;

	// Use this for initialization
	void Start () {
        Selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (GameObject.Find("CombatLogic").GetComponent<CombatLogic>().monstersTurn)
        {
            Debug.Log("Can't target enemy during enemy's turn.");
            return;
        }
        GameObject[] arrows;
        if (GameObject.Find("redarrow") != null)
        {
            arrows = GameObject.FindGameObjectsWithTag("redArrow");
            foreach (GameObject arrowObj in arrows)
            {
                arrowObj.GetComponent<ArrowScript>().Selected = false;
                if (arrowObj.GetComponent<Image>().sprite.name == "greenarrow")
                    arrowObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/redarrow");
            }
        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/greenarrow");
        Selected = true;
        if (GameObject.Find("AttackButton") == null)
        {
            GameObject attackButton = Instantiate(AttackPrefab, new Vector3(100, 100, 0), Quaternion.identity) as GameObject;
            attackButton.name = "AttackButton";
            attackButton.transform.position = new Vector3(260, -120, 0);
            attackButton.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
        }

        gameObject.name = "GreenArrow";
    }
}
