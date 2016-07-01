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
        ShowSpellAndSpellInfo( heros );
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
        canvas.AddComponent<Canvas>();
        for (int i = 0; i < 4; i++)
        {
            Vector3 vector = gameObject.transform.position;
            GameObject gameObj = Instantiate( Spell, new Vector3( vector.x, vector.y - count, vector.z ), Quaternion.identity ) as GameObject;
            gameObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( "Sprites/Combat/Characters/Spells/" + hero.Spells[i].Name );
            gameObj.name = hero.Spells[i].Name;
            //gameObj.transform.parent = canvas.transform;
            count += 0.5f;
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
}
