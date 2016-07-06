using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D.Spell;
using System.Collections.Generic;
using UnityEngine.UI;

public class HerosIni : MonoBehaviour {

    bool started = false;
    BaseHeros _heros;
    CombatManager _combat;
    public GameObject Fire;
    public GameObject Water;
    public GameObject Poison;
    public GameObject Bleeding;
    public GameObject Spell;
    Dictionary<string, GameObject> Affect = new Dictionary<string, GameObject>();
    float X = -25;
    float Y = -85;

    public void init(BaseHeros heros)
    {
        _heros = heros;
        _combat = StartCombat.Combat;
        gameObject.GetComponent<Animator>().Play(heros.CharacterClassName+"Idle", 0);

        started = true;
        Affect.Add("Fire", Fire);
        Affect.Add("Water", Water);
        Affect.Add("Poison", Poison);
        Affect.Add("Bleeding", Bleeding);
        ShowDamageOnTime();
    }
	
	// Update is called once per frame
	void Update () {

        if (started == false)
            return;

        
    }
    public void ShowSpellAndSpellInfo(BaseHeros hero)
    {
        float count = 2;
        GameObject canvas = GameObject.Find( "HeroSpells" );
        for (int i = 0; i < 4; i++)
        {
            Vector3 vector = gameObject.transform.position;
            GameObject gameObj = Instantiate( Spell, new Vector3( X, Y - count, vector.z ), Quaternion.identity ) as GameObject;
            gameObj.transform.SetParent(canvas.transform, false);
            gameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Combat/Characters/Spells/" + hero.Spells[i].Name );
            gameObj.name = hero.Spells[i].Name;
            gameObj.GetComponent<SpellInformation>().spell = hero.Spells[i];
            if (hero.Spells[i].CooldownManager.IsOnCooldown || hero.Spells[i].ManaCost > hero.Mana)
            {
                gameObj.GetComponent<Button>().enabled = false;
                gameObj.GetComponent<Image>().color = Color.grey;
            }
            count += 0.5f;

            if (i == 0 || i == 2)
                Y -= 60;
            else
            {
                Y = -85;
                X += 60;
            }
               
               
        }
        X = -25;
        Y = -85;
    }
    public void ShowDamageOnTime()
    {
        KindOfEffect effect;
        _combat.DamageOnTime.TryGetValue( _heros, out effect );
        if (effect != null)
        {
            Vector3 vector = gameObject.transform.position;

            string dtype = effect.DamageType.ToString();
            GameObject status;
            Affect.TryGetValue( dtype, out status );
            Instantiate( status, new Vector3( vector.x, vector.y - 1, vector.z ), Quaternion.identity );

        }
    }
    public void ShowSpellInformation()
    {
        Debug.Log( "Salut" );
    }

    public static void SwitchHerosGOPositions()
    {
        StartCombat sCombat = Camera.main.GetComponent<StartCombat>();
        
        for (int i = 0; i < 4; i++)
        {
            GameObject relatedGo = null;
            for (int j = 0; j < 4; j++)
            {
                if (StartCombat.Heros[i] == sCombat.herosInBattle[j])
                    relatedGo = sCombat.herosGo[j]; 
            }
            if (relatedGo != null)
            {
                relatedGo.transform.position = new Vector3(-2 - i * 2, relatedGo.transform.position.y);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject relatedGo = null;
            for (int j = 0; j < 4; j++)
            {
                if (StartCombat.Heros[i] == sCombat.herosInBattle[j])
                {
                    relatedGo = sCombat.herosGo[j];
                    GameObject GoToSwitch = sCombat.herosGo[i];
                    sCombat.herosGo[i] = relatedGo;
                    sCombat.herosGo[j] = GoToSwitch;
                    BaseHeros tmp = sCombat.herosInBattle[j];
                    sCombat.herosInBattle[j] = sCombat.herosInBattle[i];
                    sCombat.herosInBattle[i] = tmp;
                    //break;
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            sCombat.herosInBattle[i] = StartCombat.Heros[i];
        }
    }

    public static void SetMenu(BaseHeros heros)
    {

        // Setting HP
        GameObject.Find("HP").GetComponent<Text>().text = heros.HP.ToString() + "/" + heros.EffectivHPMax.ToString();

        // Setting Mana
        GameObject.Find("MANA").GetComponent<Text>().text = heros.Mana.ToString() + "/" + heros.EffectivManaMax.ToString();

        GameObject.Find("CharacterName").GetComponent<Text>().text = heros.CharacterName;
        GameObject.Find("CharacterClass").GetComponent<Text>().text = heros.CharacterClassName;
        //GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + ((heros.IsMale == true) ? "M" : "F"));
        // Pour test, rétablir la ligne précedente quand test terminé.
        GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");

        GameObject.Find("DamageT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
        GameObject.Find("HitT").GetComponent<Text>().text = heros.EffectivHitChance.ToString();
        GameObject.Find("CritT").GetComponent<Text>().text = heros.EffectCritChance.ToString();
        GameObject.Find("SpeedT").GetComponent<Text>().text = heros.EffectivSpeed.ToString();
        GameObject.Find("DefenseT").GetComponent<Text>().text = heros.EffectivDefense.ToString();
        GameObject.Find("DodgeT").GetComponent<Text>().text = heros.EffectivDodgeChance.ToString();
        GameObject.Find("MagicRT").GetComponent<Text>().text = heros.EffectivMagicRes.ToString();
        GameObject.Find("FireRT").GetComponent<Text>().text = heros.EffectivFireRes.ToString();
        GameObject.Find("PoisonRT").GetComponent<Text>().text = heros.EffectivPoisonRes.ToString();
        GameObject.Find("BleedingRT").GetComponent<Text>().text = heros.EffectivBleedingRes.ToString();
        GameObject.Find("WaterRT").GetComponent<Text>().text = heros.EffectivWaterRes.ToString();
        GameObject.Find("AffectRT").GetComponent<Text>().text = heros.EffectivAffectRes.ToString();

        float Y;
        if (heros.HP > 0)
        {
            Y = (92.5f * (100f * heros.HP / heros.EffectivHPMax)) / 100;
            GameObject.Find("HpBar2").GetComponent<RectTransform>().sizeDelta =
                new Vector2(Y, 11.2f);
        }

        if (heros.Mana > 0)
        {
            Y = (92.5f * (100f * heros.Mana / heros.EffectivManaMax)) / 100;
            GameObject.Find("ManaBar2").GetComponent<RectTransform>().sizeDelta =
                new Vector2(Y, 11.2f);
        }

    }
}
