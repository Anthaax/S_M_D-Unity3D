using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;
using S_M_D.Spell;
using System.Collections.Generic;

public class HpBarCheck : MonoBehaviour {
    public BaseMonster monster;
    int HP;
    Dictionary<string, GameObject> Affect = new Dictionary<string, GameObject>();
    public GameObject Fire;
    public GameObject Water;
    public GameObject Poison;
    public GameObject Bleeding;
    public GameObject Spell;
    private GameObject obj2;
    private KindOfEffect effect;
    public GameObject monsterGO;

    void Start()
    {
        Affect.Add("Fire", Fire);
        Affect.Add("Water", Water);
        Affect.Add("Poison", Poison);
        Affect.Add("Bleeding", Bleeding);
        HP = monster.HP;
        obj2 = null;
        effect = null;
    }
	// Update is called once per frame
	void Update () {

        KindOfEffect effect2;
        StartCombat.Combat.DamageOnTime.TryGetValue(monster, out effect2);
        ;
        if (effect2 == null)
        {
            if (obj2 != null)
                Destroy(obj2.gameObject);
        }
        if (effect2 != null && effect != effect2)
        {
            Debug.Log("Swag");
            if (obj2 != null)
                Destroy(obj2.gameObject);

            Vector3 vector = gameObject.transform.position;

            string dtype = effect2.DamageType.ToString();
            if (dtype == "Fire" || dtype == "Bleeding" || dtype == "Poison" || dtype == "Water")
            {
                GameObject status;
                Affect.TryGetValue(dtype, out status);
                obj2 = Instantiate(status, new Vector3(monsterGO.transform.position.x, monsterGO.transform.position.y - 1, 1), Quaternion.identity) as GameObject;
                Debug.Log(status);
                effect = effect2;
            }

        }

        float Y;
        if (monster.HP != HP && monster.HP>0)
        {
            

            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            Y = (40f * (100f * monster.HP / monster.EffectivHPMax)) / 100;
            obj.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Y, 3f);
            HP = monster.HP;
        }
        else if (monster.HP<=0)
        {
            int x = 0;
            foreach (GameObject O in StartCombat.monstersGO)
            {
                if (x == monster.Position)
                {
                    break;
                }
                x++;
            }
            StartCombat.monstersGO[x].GetComponent<Animator>().Play(monster.Type + "Dead");
            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            obj.SetActive(false);

        }

    }
}
