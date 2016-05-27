using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using S_M_D.Spell;

public class SpellsInfos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        GameObject.Find("Arrow1").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow2").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow3").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow4").GetComponent<Renderer>().enabled = false;

        bool[] target = new bool[4];
        string result = gameObject.name.Substring(gameObject.name.Length - 1);;
        int R = Convert.ToInt32(result);
        GameObject.Find("SpellInfo").GetComponent<Text>().text = BaseCombat.HerosPLaying.Spells[R - 1].Description;
        target = BaseCombat.HerosPLaying.Spells[R - 1].TargetManager.WhoCanBeTargetable(BaseCombat.HeroPosition);


        for (int i=0; i<4;i++)
        {
            string arrow = "Arrow"+(i + 1);
            if (target[i] == true)

                GameObject.Find(arrow).GetComponent<Renderer>().enabled = true;
        }

        GameObject.Find("Attack").GetComponent<HeroAttack>().Spell = BaseCombat.HerosPLaying.Spells[R - 1];

    }
}
