using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;

public class SpellsAndStats : MonoBehaviour {

    BaseHeros heros;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (BaseCombat.HerosPLaying !=null)
        {
            heros = BaseCombat.HerosPLaying;
            for (int i = 1; i < 5; i++)
            {
                GameObject.Find("Spell" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + BaseCombat.HerosPLaying.Spells[i-1].Name);
            }
            GameObject.Find("DamageT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivDamage.ToString();
            GameObject.Find("HitT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivHitChance.ToString();
            GameObject.Find("CritT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectCritChance.ToString();
            GameObject.Find("SpeedT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivSpeed.ToString();
            GameObject.Find("DefenseT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivDefense.ToString();
            GameObject.Find("DodgeT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivDodgeChance.ToString();
            GameObject.Find("FireRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivFireRes.ToString();
            GameObject.Find("MagicRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivMagicRes.ToString();
            GameObject.Find("PoisonRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivPoisonRes.ToString();
            GameObject.Find("BleedingRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivBleedingRes.ToString();
            GameObject.Find("WaterRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivWaterRes.ToString();
            GameObject.Find("AffectRT").GetComponent<Text>().text = BaseCombat.HerosPLaying.EffectivAffectRes.ToString();
            GameObject.Find("HP").GetComponent<Text>().text = heros.HP.ToString() + "/" + heros.EffectivHPMax.ToString();
            GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/"+heros.CharacterClassName+"IconeF");
            GameObject.Find("IconeT1").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("IconeT2").GetComponent<Text>().text = heros.CharacterClassName;
            GameObject.Find("MANA").GetComponent<Text>().text = BaseCombat.HerosPLaying.Mana.ToString()+ "/" + BaseCombat.HerosPLaying.EffectivManaMax.ToString();


            if (heros.HP>24)
            {
                heros.HP -= 1;
                float x =(92.5f *(100f * heros.HP / heros.EffectivHPMax))/100;
                GameObject.Find("HpBar2").GetComponent<RectTransform>().sizeDelta =
                    new Vector2(x, 11.2f);
            }




        }
        
	}
}
