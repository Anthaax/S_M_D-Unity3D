using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Camp.Class;

public class SetProfil : MonoBehaviour {

	
    public void Show()
    {
        string name = gameObject.name;
        int index = int.Parse(""+name[name.Length - 2]);
        BaseHeros heros = Start.Gtx.PlayerInfo.MyHeros[index - 1];
        GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        GameObject.Find("ArmorT").GetComponent<Text>().text = heros.EffectivDefense.ToString();
        GameObject.Find("AttackT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
        GameObject.Find("HitChanceT").GetComponent<Text>().text = heros.EffectivHitChance.ToString();
        GameObject.Find("CritT").GetComponent<Text>().text = heros.EffectCritChance.ToString();
        GameObject.Find("SpeedT").GetComponent<Text>().text = heros.EffectivSpeed.ToString();
        GameObject.Find("DodgeT").GetComponent<Text>().text = heros.EffectivDodgeChance.ToString();
        GameObject.Find("FireResT").GetComponent<Text>().text = heros.EffectivFireRes.ToString();
        GameObject.Find("MagicResT").GetComponent<Text>().text = heros.EffectivMagicRes.ToString();
        GameObject.Find("PoisonResT").GetComponent<Text>().text = heros.EffectivPoisonRes.ToString();
        GameObject.Find("BleedingResT").GetComponent<Text>().text = heros.EffectivBleedingRes.ToString();
        GameObject.Find("WaterResT").GetComponent<Text>().text = heros.EffectivWaterRes.ToString();
        GameObject.Find("AffectResT").GetComponent<Text>().text = heros.EffectivAffectRes.ToString();
    }
    public void ShowDispo()
    {
        string name = gameObject.name;
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingName.Caravan) as Caravan;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = caravan.HerosDispo[index - 1];
        GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        GameObject.Find("ArmorT").GetComponent<Text>().text = heros.EffectivDefense.ToString();
        GameObject.Find("AttackT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
        GameObject.Find("HitChanceT").GetComponent<Text>().text = heros.EffectivHitChance.ToString();
        GameObject.Find("CritT").GetComponent<Text>().text = heros.EffectCritChance.ToString();
        GameObject.Find("SpeedT").GetComponent<Text>().text = heros.EffectivSpeed.ToString();
        GameObject.Find("DodgeT").GetComponent<Text>().text = heros.EffectivDodgeChance.ToString();
        GameObject.Find("FireResT").GetComponent<Text>().text = heros.EffectivFireRes.ToString();
        GameObject.Find("MagicResT").GetComponent<Text>().text = heros.EffectivMagicRes.ToString();
        GameObject.Find("PoisonResT").GetComponent<Text>().text = heros.EffectivPoisonRes.ToString();
        GameObject.Find("BleedingResT").GetComponent<Text>().text = heros.EffectivBleedingRes.ToString();
        GameObject.Find("WaterResT").GetComponent<Text>().text = heros.EffectivWaterRes.ToString();
        GameObject.Find("AffectResT").GetComponent<Text>().text = heros.EffectivAffectRes.ToString();
    }
}
