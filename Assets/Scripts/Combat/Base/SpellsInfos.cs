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
        BaseHeros heros = StartCombat.Combat.GetCharacterTurn() as BaseHeros;

        bool[] target = new bool[4];
        string result = gameObject.name.Substring(gameObject.name.Length - 1);
        int R = Convert.ToInt32(result);
        GameObject.Find("SpellInfo").GetComponent<Text>().text = heros.Spells[R-1].Name+ "\n \n \n" + heros.Spells[R - 1].Description+ "\n \n \n Dégats = " + heros.Spells[R-1].KindOfEffect.Damage
                +"\n Mana : " + heros.Spells[R-1].ManaCost +"\n Cooldown : " + heros.Spells[R-1].CooldownManager.BaseCooldown +"\n Cooldown Restant : "+ heros.Spells[R-1].CooldownManager.Cooldown;

        target = heros.Spells[R - 1].TargetManager.WhoCanBeTargetable(Position(heros));


        for (int i=0; i<4;i++)
        {
            string arrow = "Arrow"+(i + 1);
            if (target[i] == true)
                if (!StartCombat.Combat.Monsters[i].IsDead)
                    if (GameObject.Find(arrow) != null)
                GameObject.Find(arrow).GetComponent<Image>().enabled = true;
        }


        StartCombat.Attack.Spell = heros.Spells[R - 1];

    }

    private int Position(BaseHeros hero)
    {
        for (int i = 0; i < StartCombat.Combat.Heros.Length; i++)
        {
            if (hero == StartCombat.Combat.Heros[i])
                return i;
        }
        return 0;
    }

}
