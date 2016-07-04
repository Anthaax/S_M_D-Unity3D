using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using S_M_D.Character;

public class CombatLogic : MonoBehaviour {

    public float timeLeft = 30.0f;
    public bool monstersTurn = false;

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
    public void DoMonstersAction(BaseMonster monster)
    {
        int[] charsOldLife = new int[4];
        for (int i = 0; i < 4; i++)
        {
            charsOldLife[i] = Camera.main.GetComponent<StartCombat>().herosInBattle[i].HP;
        }
        monstersTurn = true;
        BaseCharacter nextChar = StartCombat.Combat.IaMonster.MonsterTurnAndDoNextTurn(monster);

        int[] charsNewLife = new int[4];
        for (int i = 0; i < 4; i++)
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
}
