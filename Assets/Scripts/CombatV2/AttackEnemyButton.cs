using UnityEngine;
using System.Collections;
using S_M_D.Spell;
using S_M_D.Character;
using UnityEngine.UI;

public class AttackEnemyButton : MonoBehaviour {

    Spells spell;
    int position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        GameObject Arrow = GameObject.Find("GreenArrow");
        if (Arrow)
        {
            spell = Arrow.GetComponent<ArrowScript>().AssociatedSpell;
            position = Arrow.GetComponent<ArrowScript>().MonsterPosition;
            int i = 0;
            int[] Hp = new int[4];
            foreach (BaseMonster M in StartCombat.Combat.Monsters)
            {
                Hp[i] = M.HP;
                i++;
            }

            StartCombat.Combat.SpellManager.HeroLaunchSpell(spell, position);

            i = 0;
            foreach (BaseMonster M in StartCombat.Combat.Monsters)
            {
                Hp[i] = Hp[i] - M.HP;
                if (Hp[i] != 0)
                {
                    GameObject text = CombatLogic.AddTextToCanvas();
                    text.GetComponent<Text>().text = "-" + (Hp[i]).ToString();
                    text.transform.position = new Vector3(StartCombat.monstersGO[i].transform.position.x + i * 65, StartCombat.monstersGO[i].transform.position.y + 10, 0);
                    text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                    GameObject[] arrows;
                    if (GameObject.Find("redarrow") != null)
                    {
                        arrows = GameObject.FindGameObjectsWithTag("redArrow");
                        foreach (GameObject arrowObj in arrows)
                        {
                            Destroy(arrowObj.gameObject);
                        }
                    }
                    StartCoroutine(SelfDestroyTextHero(text, 2.0f));
                }

                i++;
             }            
            }               

    }
    public IEnumerator SelfDestroyTextHero(GameObject text, float delay)
    {
        Debug.Log("Destroying text");
        Debug.Log(text.GetComponent<Text>().text);
        yield return new WaitForSeconds(delay);
        Destroy(text.gameObject);
        GameObject.Find("Pass").GetComponent<PassTurnButton>().OnClick();

    }
}
