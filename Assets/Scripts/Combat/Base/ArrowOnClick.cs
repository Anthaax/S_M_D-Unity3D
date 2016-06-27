using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using S_M_D.Spell;

public class ArrowOnClick : MonoBehaviour {

	public void OnClick()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GameObject.Find( "Arrow" + i ) != null || !BaseCombat.Combat.Monsters[i - 1].IsDead)
            {
                GameObject.Find( "Arrow" + i ).GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Combat/1024px-Red_Arrow_Down.svg" );
            }
            else if (BaseCombat.Attack.Spell.KindOfEffect.DamageType == DamageTypeEnum.Heal || BaseCombat.Attack.Spell.KindOfEffect.DamageType == DamageTypeEnum.Move)
            {
                //ajout de fleche pour les heros
            }
            else if (BaseCombat.Combat.Monsters[i].IsDead)
                GameObject.Find( "Arrow" + i ).GetComponent<Image>().enabled = false;
        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/greenarrow");
        string result = gameObject.name.Substring(gameObject.name.Length - 1);
        int R = Convert.ToInt32(result);
        BaseCombat.Attack.Target = R-1;
    }
}
