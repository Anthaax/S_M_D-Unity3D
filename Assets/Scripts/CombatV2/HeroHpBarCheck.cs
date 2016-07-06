using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;
using S_M_D.Spell;
using System.Collections.Generic;

public class HeroHpBarCheck : MonoBehaviour
{
    int HP;
    Dictionary<string, GameObject> Affect = new Dictionary<string, GameObject>();
    public GameObject Fire;
    public GameObject Water;
    public GameObject Poison;
    public GameObject Bleeding;
    public GameObject Spell;
    private GameObject obj2;
    private KindOfEffect effect;
    public GameObject heroGo;
    public BaseHeros Hero;

    void Start()
    {
        Affect.Add("Fire", Fire);
        Affect.Add("Water", Water);
        Affect.Add("Poison", Poison);
        Affect.Add("Bleeding", Bleeding);
        HP = 100;
        obj2 = null;
        effect = null;
    }
    // Update is called once per frame
    void Update()
    {

        if (Hero == null)
        {
            return;
        }
        KindOfEffect effect2;
        StartCombat.Combat.DamageOnTime.TryGetValue(Hero, out effect2);
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
                obj2 = Instantiate(status, new Vector3(heroGo.transform.position.x, heroGo.transform.position.y - 1, 1), Quaternion.identity) as GameObject;
                Debug.Log(status);
                effect = effect2;
            }

        }
        if (obj2 != null)
        {
            obj2.transform.position = new Vector3(heroGo.transform.position.x, heroGo.transform.position.y - 1, 1);
        }
        float Y;
        if (Hero.HP != HP && Hero.HP > 0)
        {


            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            Y = (40f * (100f * Hero.HP / Hero.EffectivHPMax)) / 100;
            obj.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Y, 3f);
            HP = Hero.HP;
        }
        else if (Hero.HP <= 0)
        {

        }


        if (heroGo.transform.position.x == -2)
        {
            gameObject.transform.position = new Vector3(787.2f, 550.5f, 1);
        }
        else if
            (heroGo.transform.position.x == -4)
        {
            gameObject.transform.position = new Vector3(614.3999f, 550.5f, 1);
        }
        else if
            (heroGo.transform.position.x == -6)
        {
            gameObject.transform.position = new Vector3(441.5999f, 550.5f, 1);
        }
        else if
            (heroGo.transform.position.x == -8)
        {
            gameObject.transform.position = new Vector3(268.7999f, 550.5f, 1);
        }
    }
}
