using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using S_M_D.Spell;
using S_M_D.Character;

public class SpellInformation : MonoBehaviour {

    public Spells spell;
    int position;
    bool[] cible;

    public GameObject arrowPrefab;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        position = 0;
        foreach(BaseHeros H in StartCombat.Combat.Heros)
        {
            if (H == StartCombat.Combat.GetCharacterTurn())
            {
                break;
            }
                position++;
        }
        cible = StartCombat.Combat.SpellManager.WhoCanBeTargetable(spell, position);
        int i = 0;
        foreach(bool b in cible)
        {
            if (b)
                Instantiate(arrowPrefab, new Vector3(StartCombat.monstersGO[i].transform.position.x, StartCombat.monstersGO[i].transform.position.y + 1, 0), Quaternion.identity);
              i++;
        }
    }
}
