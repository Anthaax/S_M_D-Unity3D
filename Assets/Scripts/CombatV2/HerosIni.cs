using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D.Spell;
using System.Collections.Generic;

public class HerosIni : MonoBehaviour {

    bool started = false;
    BaseHeros _heros;
    CombatManager _combat;
    public GameObject Fire;
    public GameObject Water;
    public GameObject Poison;
    public GameObject Bleeding;
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
      //  _combat.DamageOnTime.Add(heros, StartCombat.Gtx.PlayerInfo.MyHeros[0].Spells[1].KindOfEffect);
        Debug.Log(_combat.DamageOnTime.Count);
        _combat.DamageOnTime[heros] = StartCombat.Gtx.PlayerInfo.MyHeros[0].Spells[1].KindOfEffect;



    }
	
	// Update is called once per frame
	void Update () {

        if (started == false)
            return;

        KindOfEffect effect;
        _combat.DamageOnTime.TryGetValue(_heros, out effect);
        if(effect != null)
        {
            Vector3 swag = gameObject.transform.position;

            string dtype = effect.DamageType.ToString();
            GameObject status;
            Affect.TryGetValue(dtype, out status);
            Instantiate(status, new Vector3(swag.x, swag.y-1, swag.z), Quaternion.identity);

        }

	}
}
