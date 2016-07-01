using UnityEngine;
using System.Collections;
using S_M_D.Character;

public class MonsterIni : MonoBehaviour {

    bool started = false;
    BaseMonster _monster;

    public void init(BaseMonster monster)
    {
        _monster = monster;
        Animator animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("Animations/CombatV2" + _monster.Type) as RuntimeAnimatorController;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (started == false)
            return;

    }
}
