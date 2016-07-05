using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;

public class HpBarCheck : MonoBehaviour {
    public BaseMonster monster;
    int HP;
	
    void Start()
    {
        HP = monster.HP;
    }
	// Update is called once per frame
	void Update () {      
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
            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            obj.SetActive(false);

        }

    }
}
