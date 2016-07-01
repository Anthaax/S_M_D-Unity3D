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
        Animator animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("Animations/CombatV2"+heros.CharacterClassName) as RuntimeAnimatorController;
        started = true;
        Affect.Add("Fire", Fire);
        Affect.Add("Water", Water);
        Affect.Add("Poison", Poison);
        Affect.Add("Bleeding", Bleeding);
        Debug.Log(_combat.DamageOnTime.Count);
        _combat.DamageOnTime[heros] = StartCombat.Gtx.PlayerInfo.MyHeros[0].Spells[1].KindOfEffect;
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
        GameObject canvas = GameObject.Find( "SuperCanvas" );
        for (int i = 0; i < 4; i++)
        {
            Vector3 vector = gameObject.transform.position;
            GameObject gameObj = Instantiate( Spell, new Vector3( X, Y - count, vector.z ), Quaternion.identity ) as GameObject;
            gameObj.transform.SetParent(canvas.transform, false);
            Debug.Log(hero);
            gameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Combat/Characters/Spells/" + hero.Spells[i].Name );
            gameObj.name = hero.Spells[i].Name;
            gameObj.GetComponent<SpellInformation>().spell = hero.Spells[i];
            count += 0.5f;

            if (i == 0 || i == 2)
                Y -= 60;
            else
            {
                Y = -85;
                X += 60;
            }
               
               
        }
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

    public void SetMenu(BaseHeros heros)
    {
        GameObject.Find("DamageT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
    }
}
