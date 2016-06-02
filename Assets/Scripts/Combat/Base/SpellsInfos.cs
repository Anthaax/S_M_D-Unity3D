using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using S_M_D.Spell;
using S_M_D.Character;

public class SpellsInfos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GameObject.Find("Arrow" + i) != null)
            {
                GameObject.Find("Arrow" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/1024px-Red_Arrow_Down.svg");
                GameObject.Find("Arrow"+i).GetComponent<Image>().enabled = false;
            }
        }
        BaseHeros heros = BaseCombat.Combat.GetCharacterTurn() as BaseHeros;

        bool[] target = new bool[4];
        string result = gameObject.name.Substring(gameObject.name.Length - 1);
        int R = Convert.ToInt32(result);
        GameObject.Find("SpellInfo").GetComponent<Text>().text = heros.Spells[R-1].Name+ "\n \n \n" + heros.Spells[R - 1].Description;
        target = heros.Spells[R - 1].TargetManager.WhoCanBeTargetable(Position(heros));


        for (int i=0; i<4;i++)
        {
            string arrow = "Arrow"+(i + 1);
            if (target[i] == true)
                if (GameObject.Find(arrow) != null)
                GameObject.Find(arrow).GetComponent<Image>().enabled = true;
        }


        BaseCombat.Attack.Spell = heros.Spells[R - 1];

    }

    private int Position(BaseHeros hero)
    {
        for (int i = 0; i < BaseCombat.Combat.Heros.Length; i++)
        {
            if (hero == BaseCombat.Combat.Heros[i])
                return i;
        }
        return 0;
    }

}
