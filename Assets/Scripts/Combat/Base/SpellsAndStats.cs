using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;
using System.Collections.Generic;
using S_M_D.Spell;
using System.Linq;

public class SpellsAndStats : MonoBehaviour {
    public static int count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        BaseHeros heros = BaseCombat.Combat.GetCharacterTurn() as BaseHeros;
        if (heros !=null)
        {
            for (int i = 1; i < 5; i++)
            {
                if(heros.Spells[i-1].IsEquiped)
                    GameObject.Find("Spell" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" +heros.Spells[i-1].Name);
                else
                    GameObject.Find("Spell" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/NoSpell");

                if (heros.Mana < heros.Spells[i - 1].ManaCost || heros.Spells[i - 1].CooldownManager.IsOnCooldown || !heros.Spells[i - 1].IsEquiped)
                {
                    GameObject.Find( "Spell" + i ).GetComponent<Button>().enabled = false;
                    GameObject.Find( "Spell" + i ).GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    GameObject.Find( "Spell" + i ).GetComponent<Button>().enabled = true;
                    GameObject.Find( "Spell" + i ).GetComponent<Image>().color = Color.white;
                }
            }
            GameObject.Find("DamageT").GetComponent<Text>().text =heros.EffectivDamage.ToString();
            GameObject.Find("HitT").GetComponent<Text>().text =heros.EffectivHitChance.ToString();
            GameObject.Find("CritT").GetComponent<Text>().text =heros.EffectCritChance.ToString();
            GameObject.Find("SpeedT").GetComponent<Text>().text =heros.EffectivSpeed.ToString();
            GameObject.Find("DefenseT").GetComponent<Text>().text =heros.EffectivDefense.ToString();
            GameObject.Find("DodgeT").GetComponent<Text>().text =heros.EffectivDodgeChance.ToString();
            GameObject.Find("FireRT").GetComponent<Text>().text =heros.EffectivFireRes.ToString();
            GameObject.Find("MagicRT").GetComponent<Text>().text =heros.EffectivMagicRes.ToString();
            GameObject.Find("PoisonRT").GetComponent<Text>().text =heros.EffectivPoisonRes.ToString();
            GameObject.Find("BleedingRT").GetComponent<Text>().text =heros.EffectivBleedingRes.ToString();
            GameObject.Find("WaterRT").GetComponent<Text>().text =heros.EffectivWaterRes.ToString();
            GameObject.Find("AffectRT").GetComponent<Text>().text =heros.EffectivAffectRes.ToString();
            GameObject.Find("HP").GetComponent<Text>().text = heros.HP.ToString() + "/" + heros.EffectivHPMax.ToString();
            GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/"+heros.CharacterClassName+"IconeF");
            GameObject.Find("IconeT1").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("IconeT2").GetComponent<Text>().text = heros.CharacterClassName;
            GameObject.Find("MANA").GetComponent<Text>().text =heros.Mana.ToString()+ "/" +heros.EffectivManaMax.ToString();
        }
        count++;
        int x = 1;
        int y = 1;
        foreach (BaseMonster M in BaseCombat.Combat.Monsters)
        {
            Animator animator = GameObject.Find("Monstre" + y).GetComponent<Animator>();
            
            float Y;           
            if (M.HP > 0)               
            {
                Y = (44f * (100f * M.HP / M.HPmax)) / 100;
                GameObject.Find("MonsterHPG" + y).GetComponent<RectTransform>().sizeDelta =
                    new Vector2(Y, 7f);
                GameObject.Find("MonsterHPT" + y).GetComponent<Text>().text = M.HP.ToString() + "/" + M.HPmax.ToString();
            }
            
            else
            {
                Y = 0;
                GameObject.Find("MonsterHPT" + y).GetComponent<Text>().text = "Dead";
                GameObject.Find("MonsterHPG" + y).GetComponent<Image>().color = Color.red;
                animator.SetBool("IsDead", true);
            }
                
            y++;
        }
    }
}
