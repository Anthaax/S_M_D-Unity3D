using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using S_M_D.Character;
using UnityEngine.Networking;
using System;
using S_M_D.Spell;
using System.Linq;
using S_M_D.Character.Monsters;

public class CombatLogic : NetworkBehaviour
{

    public float timeLeft = 30.0f;
    public bool monstersTurn = false;
    public string playersTurn;

    // Client will get the monsters from the server
    void Start()
    {
        if (!isServer)
        {
            Cmd_AskServerMonsters();
        }
    }

    void Update()
    {
        if (!monstersTurn)
            timeLeft -= Time.deltaTime;
        //CombatIsOver();
        if (timeLeft <= 0)
        {
            //StartCombat.Combat.NextTurn();
            //timeLeft = 30.0f;
            GameObject.Find("Pass").GetComponent<PassTurnButton>().OnClick();
        }
        GameObject.Find("TimerText").GetComponent<Text>().text = timeLeft.ToString();
        if (StartCombat.Combat.GetCharacterTurn() is BaseHeros)
            playersTurn = ((BaseHeros)StartCombat.Combat.GetCharacterTurn()).Owner;
    }

    [Command]
    public void Cmd_AskServerMonsters()
    {
        string[] monstersType = new string[4];
        int[] monstersLevel = new int[4];
        for (int i = 0; i < 4; i++)
        {
            monstersType[i] = "null";
            monstersLevel[i] = 0;
        }
        int idx = 0;
        foreach (BaseMonster M in StartCombat.Combat.Monsters)
        {
            monstersType[idx] = M.Type.ToString();
            monstersLevel[idx++] = M.Lvl;
        }
        Rpc_SendClientMonsters(monstersType[0], monstersType[1], monstersType[2], monstersType[3], monstersLevel[0], monstersLevel[1], monstersLevel[2], monstersLevel[3]);
    }

    [ClientRpc]
    public void Rpc_SendClientMonsters(string m1type, string m2type, string m3type, string m4type, int m1lvl, int m2lvl, int m3lvl, int m4lvl)
    {
        string[] monstersType = new string[4];
        int[] monstersLevel = new int[4];
        monstersType[0] = m1type;
        monstersType[1] = m2type;
        monstersType[2] = m3type;
        monstersType[3] = m4type;
        monstersLevel[0] = m1lvl;
        monstersLevel[1] = m2lvl;
        monstersLevel[2] = m3lvl;
        monstersLevel[3] = m4lvl;
        MonsterConfiguration mconfig = new MonsterConfiguration();
        for (int i = 0; i < 4; i++)
        {
            MonsterType type = (MonsterType)Enum.Parse(typeof(MonsterType), monstersType[i]);
            StartCombat.Combat.Monsters[i] = mconfig.CreateMonster(type, monstersLevel[i]);
        }
        StartCombat.Combat.CharacterOrderAttack.Clear();
        StartCombat.Combat.InitializedOderAttack();
        Debug.Log("INITIALIZATION MONSTERS");
        for (int i = 0; i < 4; i++)
        {
            StartCombat.monstersGO[i].GetComponent<Animator>().Play(monstersType[i] + "Idle", 0);
            Camera.main.GetComponent<StartCombat>().monstersHpBar[i].GetComponent<HpBarCheck>().monster = StartCombat.Combat.Monsters[i];
        }

    }

    [ClientRpc]
    public void Rpc_ClientCombatEnd()
    {
        SceneManager.LoadScene(2);
        BoardManager.Map = StartCombat.Map;
        BoardManager.Gtx = StartCombat.Gtx;
        BoardManager.hero = StartCombat.Heros;
    }

    [Command]
    public void Cmd_ServerCombatEnd()
    {
        SceneManager.LoadScene(2);
        BoardManager.Map = StartCombat.Map;
        BoardManager.Gtx = StartCombat.Gtx;
        BoardManager.hero = StartCombat.Heros;
    }

    public void CombatEnd()
    {
        if (!isServer)
        {
            Cmd_ServerCombatEnd();
            SceneManager.LoadScene(2);
            BoardManager.Map = StartCombat.Map;
            BoardManager.Gtx = StartCombat.Gtx;
            BoardManager.hero = StartCombat.Heros;
        }
        else
        {
            SceneManager.LoadScene(2);
            BoardManager.Map = StartCombat.Map;
            BoardManager.Gtx = StartCombat.Gtx;
            BoardManager.hero = StartCombat.Heros;
            Rpc_ClientCombatEnd();
        }
    }

    public IEnumerator TemporizeMonstersAction(BaseMonster M, float waitTimeInSecs, int monsterPos, BaseCharacter nextChar)
    {

        yield return new WaitForSeconds(waitTimeInSecs);
        monstersTurn = false;
        // Basic check
        if (monsterPos < 4)
            StartCombat.monstersGO[monsterPos].GetComponent<Animator>().Play(M.Type + "Idle", 0);
        HerosIni.SwitchHerosGOPositions();

        // Leave the fight
        if (StartCombat.Combat.CheckIfTheCombatWasOver() && isServer)
        {
            CombatEnd();
        }

        if (nextChar is BaseMonster)
            DoMonstersAction((BaseMonster)nextChar);
        else
        {
            Camera.main.GetComponent<StartCombat>().ChangeSpells((BaseHeros)nextChar);
            HerosIni.SetMenu((BaseHeros)nextChar);
        }
    }

    public IEnumerator SelfDestroyText(GameObject text, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(text.gameObject);
    }

    public IEnumerator DamageAnim(BaseHeros H, float delay)
    {
        GameObject Object = null;
        foreach (GameObject obj in Camera.main.GetComponent<StartCombat>().herosGo)
        {
            if (obj.name == H.CharacterName)
            {
                Object = obj;
            }
        }
        if (Object != null)
        {
            Object.GetComponent<Animator>().Play(H.CharacterClassName + "Hurt", 0);
            yield return new WaitForSeconds(delay);
            if (H.HP <= 0)
            {
                Object.GetComponent<Animator>().Play(H.CharacterClassName + "Dead", 0);
            }
            else
            {
                Object.GetComponent<Animator>().Play(H.CharacterClassName + "Idle", 0);
            }

        }

    }


    public static GameObject AddTextToCanvas()
    {

        GameObject textGO = new GameObject("TextGO");

        Text text = textGO.AddComponent<Text>();

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.material = ArialFont.material;

        return textGO;
    }

    public GameObject GetHeroGORelatedToHero(BaseHeros hero)
    {
        for (int i = 0; i < 4; i++)
        {
            if (StartCombat.Heros[i] == hero)
                return Camera.main.GetComponent<StartCombat>().herosGo[i];
        }
        return null;
    }

    [Command]
    public void Cmd_ApplyDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement, string effect1, string effect2, string effect3, string effect4)
    {
        int[] herosHp = new int[4];
        herosHp[0] = hero1HP;
        herosHp[1] = hero2HP;
        herosHp[2] = hero3HP;
        herosHp[3] = hero4HP;
        int[] herosPos = new int[4];
        herosPos[0] = hero1Movement;
        herosPos[1] = hero2Movement;
        herosPos[2] = hero3Movement;
        herosPos[3] = hero4Movement;
        for (int i = 0; i < 4; i++)
        {
            if ((herosHp[i]) != 0)
            {
                GameObject text = CombatLogic.AddTextToCanvas();
                text.GetComponent<Text>().text = "-" + herosHp[i].ToString();
                GameObject heroGo = GetHeroGORelatedToHero(StartCombat.Heros[i]);
                text.transform.position = new Vector3(heroGo.transform.position.x - i * 65, heroGo.transform.position.y + 10, 0);
                text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                StartCoroutine(SelfDestroyText(text, 2.0f));
                StartCoroutine(DamageAnim(StartCombat.Heros[i], 2f));
                StartCombat.Combat.Heros[i].HP -= Math.Abs(herosHp[i]);
            }
        }
        BaseHeros[] herosFinaux = new BaseHeros[4];
        int j;
        for (j = 0; j < 4; j++)
        {
            herosFinaux[j + herosPos[j]] = StartCombat.Combat.Heros[j];
        }
        for (int i = 0; i < 4; i++)
        {
            StartCombat.Combat.Heros[i] = herosFinaux[i];
        }
        string[] effects = new string[4];
        effects[0] = effect1;
        effects[1] = effect2;
        effects[2] = effect3;
        effects[3] = effect4;
        for (int i = 0; i < 4; i++)
        {
            if (effects[i] != "null")
            {
                DamageTypeEnum damagetype = (DamageTypeEnum)Enum.Parse(typeof(DamageTypeEnum), effects[i]);
                KindOfEffect e = null;
                foreach (BaseMonster M in StartCombat.Combat.Monsters)
                {
                    foreach (Spells S in M.Spells)
                    {
                        if (S.KindOfEffect.DamageType == damagetype)
                            e = S.KindOfEffect;
                    }
                }
                if (e != null)
                    StartCombat.Combat.DamageOnTime[StartCombat.Combat.Heros[i]] = e;
            }
        }
    }

    [ClientRpc]
    public void Rpc_ApplyDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement, string effect1, string effect2, string effect3, string effect4)
    {
        int[] herosHp = new int[4];
        herosHp[0] = hero1HP;
        herosHp[1] = hero2HP;
        herosHp[2] = hero3HP;
        herosHp[3] = hero4HP;
        int[] herosPos = new int[4];
        herosPos[0] = hero1Movement;
        herosPos[1] = hero2Movement;
        herosPos[2] = hero3Movement;
        herosPos[3] = hero4Movement;
        for (int i = 0; i < 4; i++)
        {
            if ((herosHp[i]) != 0)
            {
                GameObject text = CombatLogic.AddTextToCanvas();
                text.GetComponent<Text>().text = "-" + herosHp[i].ToString();
                GameObject heroGo = GetHeroGORelatedToHero(StartCombat.Heros[i]);
                text.transform.position = new Vector3(heroGo.transform.position.x - i * 65, heroGo.transform.position.y + 10, 0);
                text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                StartCoroutine(SelfDestroyText(text, 2.0f));
                StartCoroutine(DamageAnim(StartCombat.Heros[i], 2f));
                StartCombat.Combat.Heros[i].HP -= Math.Abs(herosHp[i]);
            }
        }
        BaseHeros[] herosFinaux = new BaseHeros[4];
        int j;
        for (j = 0; j < 4; j++)
        {
            herosFinaux[j + herosPos[j]] = StartCombat.Combat.Heros[j];
        }
        for (int i = 0; i < 4; i++)
        {
            StartCombat.Combat.Heros[i] = herosFinaux[i];
        }
        string[] effects = new string[4];
        effects[0] = effect1;
        effects[1] = effect2;
        effects[2] = effect3;
        effects[3] = effect4;
        for (int i = 0; i < 4; i++)
        {
            if (effects[i] != "null")
            {
                DamageTypeEnum damagetype = (DamageTypeEnum)Enum.Parse(typeof(DamageTypeEnum), effects[i]);
                KindOfEffect e = null;
                foreach (BaseMonster M in StartCombat.Combat.Monsters)
                {
                    foreach (Spells S in M.Spells)
                    {
                        if (S.KindOfEffect.DamageType == damagetype)
                            e = S.KindOfEffect;
                    }
                }
                if (e != null)
                    StartCombat.Combat.DamageOnTime[StartCombat.Combat.Heros[i]] = e;
            }
        }
    }

    public void SendDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement, string effect1, string effect2, string effect3, string effect4)
    {
        if (isServer)
        {
            Rpc_ApplyDamagesAndPositions(hero1HP, hero2HP, hero3HP, hero4HP, hero1Movement, hero2Movement, hero3Movement, hero4Movement, effect1, effect2, effect3, effect4);
        }
        else
        {
            Cmd_ApplyDamagesAndPositions(hero1HP, hero2HP, hero3HP, hero4HP, hero1Movement, hero2Movement, hero3Movement, hero4Movement, effect1, effect2, effect3, effect4);
        }
    }

    public void DoMonstersAction(BaseMonster monster)
    {
        int[] charsOldLife = new int[4];
        for (int a = 0; a < 4; a++)
        {
            charsOldLife[a] = Camera.main.GetComponent<StartCombat>().herosInBattle[a].HP;
        }
        monstersTurn = true;
        BaseHeros[] oldHeros = new BaseHeros[4];
        int i = 0;
        foreach (BaseHeros h in StartCombat.Heros)
            oldHeros[i++] = h;
        BaseCharacter nextChar = StartCombat.Combat.IaMonster.MonsterTurnAndDoNextTurn(monster);
        BaseHeros[] newHeros = new BaseHeros[4];
        i = 0;
        foreach (BaseHeros h in StartCombat.Heros)
            newHeros[i++] = h;
        int[] herosMovement = new int[4];
        int j = 0;
        foreach (BaseHeros H in oldHeros)
        {
            int k = 0;
            foreach (BaseHeros H2 in newHeros)
            {
                if (H == H2)
                {
                    herosMovement[j] = (k - j);
                    Debug.Log("Hero " + j + " deplacement = " + herosMovement[j]);
                }
                k++;
            }
            j++;
        }
        int[] charsNewLife = new int[4];
        for (int z = 0; z < 4; z++)
        {
            charsNewLife[z] = Camera.main.GetComponent<StartCombat>().herosInBattle[z].HP;
        }
        string[] effects = new string[4];
        int hi = 0;
        foreach (BaseHeros H in StartCombat.Heros)
        {
            KindOfEffect effect;
            StartCombat.Combat.DamageOnTime.TryGetValue(H, out effect);
            if (effect != null)
                effects[hi] = effect.DamageType.ToString();
            else
                effects[hi] = "null";
            hi++;
        }
        SendDamagesAndPositions(charsOldLife[0] - charsNewLife[0],
                                charsOldLife[1] - charsNewLife[1],
                                charsOldLife[2] - charsNewLife[2],
                                charsOldLife[3] - charsNewLife[3],
                                herosMovement[0], herosMovement[1], herosMovement[2], herosMovement[3],
                                effects[0], effects[1], effects[2], effects[3]);
        for (i = 0; i < 4; i++)
        {
            charsNewLife[i] = Camera.main.GetComponent<StartCombat>().herosInBattle[i].HP;
            if ((charsOldLife[i] - charsNewLife[i]) != 0)
            {
                GameObject text = CombatLogic.AddTextToCanvas();
                text.GetComponent<Text>().text = (charsOldLife[i] - charsNewLife[i]).ToString();
                GameObject heroGo = GetHeroGORelatedToHero(StartCombat.Heros[i]);
                text.transform.position = new Vector3(heroGo.transform.position.x - i * 65, heroGo.transform.position.y + 10, 0);
                text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                StartCoroutine(SelfDestroyText(text, 2.0f));
                StartCoroutine(DamageAnim(StartCombat.Heros[i], 2f));
            }
        }


        int monsterGoPositionInFight = 0;
        for (monsterGoPositionInFight = 0; monsterGoPositionInFight < 4; monsterGoPositionInFight++)
        {
            if (Camera.main.GetComponent<StartCombat>().monstersInFight[monsterGoPositionInFight] == monster)
                break;
        }
        // Basic check
        if (monsterGoPositionInFight < 4)
            StartCombat.monstersGO[monsterGoPositionInFight].GetComponent<Animator>().Play(monster.Type + "Attack", 0);
        StartCoroutine(TemporizeMonstersAction(monster, 1.0f, monsterGoPositionInFight, nextChar));
    }

    [Command]
    public void Cmd_AttackMonster(int monster1Hp, int monster2Hp, int monster3Hp, int monster4Hp, int monster1Dot, int monster2Dot,
                                  int monster3Dot, int monster4Dot, string monster1Effect, string monster2Effect, string monster3Effect, string monster4Effect)
    {
        int[] monstersHp = new int[4];
        monstersHp[0] = monster1Hp;
        monstersHp[1] = monster2Hp;
        monstersHp[2] = monster3Hp;
        monstersHp[3] = monster4Hp;
        for (int i = 0; i < 4; i++)
        {
            if (monstersHp[i] != 0)
            {
                GameObject text = CombatLogic.AddTextToCanvas();
                text.GetComponent<Text>().text = "-" + (monstersHp[i]).ToString();
                text.transform.position = new Vector3(StartCombat.monstersGO[i].transform.position.x + i * 65, StartCombat.monstersGO[i].transform.position.y + 10, 0);
                text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                StartCoroutine(SelfDestroyText(text, 2.0f));
                StartCombat.Combat.Monsters[i].HP -= Math.Abs(monstersHp[i]);
            }
        }
        string[] monstersEffects = new string[4];
        monstersEffects[0] = monster1Effect;
        monstersEffects[1] = monster2Effect;
        monstersEffects[2] = monster3Effect;
        monstersEffects[3] = monster4Effect;
        for (int i = 0; i < 4; i++)
        {
            if (monstersEffects[i] != "null")
            {
                DamageTypeEnum effect = (DamageTypeEnum)Enum.Parse(typeof(DamageTypeEnum), monstersEffects[i]);
                BaseMonster M = StartCombat.Combat.Monsters[i];
                KindOfEffect koEffect;
                if ((koEffect = GetKindOfEffect(effect)) != null)
                    StartCombat.Combat.DamageOnTime[M] = koEffect;
            }
        }
    }

    public KindOfEffect GetKindOfEffect(DamageTypeEnum damage)
    {
        foreach (var hero in StartCombat.Heros)
        {
            foreach (var spell in hero.Spells)
            {
                if (spell.KindOfEffect.DamageType == damage)
                {
                    return spell.KindOfEffect;
                }
            }
        }
        return null;
    }
    [ClientRpc]
    public void Rpc_AttackMonster(int monster1Hp, int monster2Hp, int monster3Hp, int monster4Hp, int monster1Dot, int monster2Dot,
                                  int monster3Dot, int monster4Dot, string monster1Effect, string monster2Effect, string monster3Effect, string monster4Effect)
    {
        int[] monstersHp = new int[4];
        monstersHp[0] = monster1Hp;
        monstersHp[1] = monster2Hp;
        monstersHp[2] = monster3Hp;
        monstersHp[3] = monster4Hp;
        for (int i = 0; i < 4; i++)
        {
            if (monstersHp[i] != 0)
            {
                GameObject text = CombatLogic.AddTextToCanvas();
                text.GetComponent<Text>().text = "-" + (monstersHp[i]).ToString();
                text.transform.position = new Vector3(StartCombat.monstersGO[i].transform.position.x + i * 65, StartCombat.monstersGO[i].transform.position.y + 10, 0);
                text.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
                StartCoroutine(SelfDestroyText(text, 2.0f));
                StartCombat.Combat.Monsters[i].HP -= Math.Abs(monstersHp[i]);
            }
        }
        string[] monstersEffects = new string[4];
        monstersEffects[0] = monster1Effect;
        monstersEffects[1] = monster2Effect;
        monstersEffects[2] = monster3Effect;
        monstersEffects[3] = monster4Effect;
        for (int i = 0; i < 4; i++)
        {
            if (monstersEffects[i] != "null")
            {
                DamageTypeEnum effect = (DamageTypeEnum)Enum.Parse(typeof(DamageTypeEnum), monstersEffects[i]);
                BaseMonster M = StartCombat.Combat.Monsters[i];
                KindOfEffect koEffect;
                if ((koEffect = GetKindOfEffect(effect)) != null)
                    StartCombat.Combat.DamageOnTime[M] = koEffect;
            }
        }
    }

    public void AttackMonster(int monster1Hp, int monster2Hp, int monster3Hp, int monster4Hp, int monster1Dot, int monster2Dot,
                              int monster3Dot, int monster4Dot, string monster1Effect, string monster2Effect, string monster3Effect, string monster4Effect)
    {
        if (isServer)
        {
            Rpc_AttackMonster(monster1Hp, monster2Hp, monster3Hp, monster4Hp, monster1Dot, monster2Dot, monster3Dot, monster4Dot, monster1Effect, monster2Effect, monster3Effect, monster4Effect);
        }
        else
        {
            Cmd_AttackMonster(monster1Hp, monster2Hp, monster3Hp, monster4Hp, monster1Dot, monster2Dot, monster3Dot, monster4Dot, monster1Effect, monster2Effect, monster3Effect, monster4Effect);
        }
    }

    private void CombatIsOver()
    {
        if (StartCombat.Combat.CheckIfTheCombatWasOver())
        {
            SceneManager.LoadScene(2);

            BoardManager.Map = StartCombat.Map;
            BoardManager.Gtx = StartCombat.Gtx;
            BoardManager.hero = StartCombat.Heros;
            BoardManager.Gtx.DungeonManager.CbtManager.ApplyRewward();
        }
    }

    [Command]
    public void Cmd_PassTurn()
    {
        GameObject.Find("Pass").GetComponent<PassTurnButton>().OnClick();
    }

    [ClientRpc]
    public void Rpc_PassTurn()
    {
        GameObject.Find("Pass").GetComponent<PassTurnButton>().OnClick();
    }

    public void PassTurnForOtherPlayer()
    {
        if (isServer)
            Rpc_PassTurn();
        else
            Cmd_PassTurn();
    }
}
