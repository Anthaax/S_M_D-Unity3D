using UnityEngine;
using System.Collections;
using S_M_D.Character;

public class MonsterIni : MonoBehaviour {

    bool started = false;
    BaseMonster _monster;

    public void init(BaseMonster monster)
    {
        _monster = monster;
        Debug.Log("swag");
        gameObject.GetComponent<Animator>().Play(monster.Type + "Idle", 0);
        started = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (started == false)
            return;

    }
}
