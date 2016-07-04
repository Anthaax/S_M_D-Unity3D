using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using S_M_D.Character;
using UnityEngine.Networking;
using System;
using S_M_D.Spell;
using System.Linq;

public class CombatLogic : NetworkBehaviour {

    public float timeLeft = 30.0f;
    public bool monstersTurn = false;
    public string playersTurn;

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

    public IEnumerator TemporizeMonstersAction(float waitTimeInSecs, int monsterPos, BaseCharacter nextChar)
    {

        yield return new WaitForSeconds(waitTimeInSecs);
        monstersTurn = false;
        // Basic check
        if (monsterPos < 4)
            StartCombat.monstersGO[monsterPos].GetComponent<Animator>().Play("OrcIdle");
        HerosIni.SwitchHerosGOPositions();
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
    public void Cmd_ApplyDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement)
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
            }
        }
        BaseHeros[] herosFinaux = new BaseHeros[4];
        int pos;
        for (pos = 0; pos < 4; pos++)
        {
            herosFinaux[pos + herosPos[pos]] = StartCombat.Combat.Heros[pos];
        }
        for (int i = 0; i < 4; i++)
        {
            StartCombat.Combat.Heros[i] = herosFinaux[i];
        }
    }

    [ClientRpc]
    public void Rpc_ApplyDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement)
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
        
    }

    public void SendDamagesAndPositions(int hero1HP, int hero2HP, int hero3HP, int hero4HP, int hero1Movement, int hero2Movement, int hero3Movement, int hero4Movement)
    {
        if (isServer)
        {
            Rpc_ApplyDamagesAndPositions(hero1HP, hero2HP, hero3HP, hero4HP, hero1Movement, hero2Movement, hero3Movement, hero4Movement);
        }
        else
        {
            Cmd_ApplyDamagesAndPositions(hero1HP, hero2HP, hero3HP, hero4HP, hero1Movement, hero2Movement, hero3Movement, hero4Movement);
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
        foreach(BaseHeros H in oldHeros)
        {
            int k = 0;
            foreach(BaseHeros H2 in newHeros)
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
        SendDamagesAndPositions(charsNewLife[0] - charsOldLife[0],
                                charsNewLife[1] - charsOldLife[1],
                                charsNewLife[2] - charsOldLife[2],
                                charsNewLife[3] - charsOldLife[3],
                                herosMovement[0], herosMovement[1], herosMovement[2], herosMovement[3]);    
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
            StartCombat.monstersGO[monsterGoPositionInFight].GetComponent<Animator>().Play("OrcAttack");
        StartCoroutine(TemporizeMonstersAction(1.0f, monsterGoPositionInFight, nextChar));
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
                StartCombat.Combat.Monsters[i].HP -= monstersHp[i];
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
                if(spell.KindOfEffect.DamageType  == damage)
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
                StartCombat.Combat.Monsters[i].HP -= monstersHp[i];
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
        if (BaseCombat.Combat.CheckIfTheCombatWasOver())
        {
            SceneManager.LoadScene(2);

            BoardManager.Map = BaseCombat.Map;
            BoardManager.Gtx = BaseCombat.Gtx;
            BoardManager.hero = BaseCombat.Heros;
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
