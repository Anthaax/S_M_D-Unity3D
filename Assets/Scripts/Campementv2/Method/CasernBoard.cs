using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp.Class;
using System.Collections.Generic;
using UnityEngine.UI;
using S_M_D.Spell;

public class CasernBoard : MonoBehaviour {


    public static void SetBoard()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;

        List<GameObject> spellsBox = Start.CasernSpells;

        for (int i = 0; i < spellsBox.Count; i++)
        {
            if (hero.Spells[i] != null)
            {
                spellsBox[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + hero.Spells[i].Name);
                spellsBox[i].GetComponentsInChildren<Text>()[0].text = hero.Spells[i].Name;

                if(hero.Spells[i].IsBuy)
                {
                    for(int j = 0; j < spellsBox[i].GetComponentsInChildren<Button>().Length; j++)
                    {
                        if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "BuySpell")
                            SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);

                    }
                }
                else
                {
                    for (int j = 0; j < spellsBox[i].GetComponentsInChildren<Button>().Length; j++)
                    {
                        if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "BuySpell")
                            SetToActiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);

                    }
                }
                CheckEquiped(i, hero.Spells[i]);
            }
            else
                break;
        }
    }

    private static void CheckEquiped(int i, Spells spell)
    {
        List<GameObject> spellsBox = Start.CasernSpells;

        if (spell.IsEquiped)
            spellsBox[i].GetComponent<Button>().image.color = Color.green;
        else
            spellsBox[i].GetComponent<Button>().image.color = Color.red;
    }

    public void setSpellEquipedOrNot()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;

        string spellName = gameObject.GetComponentsInChildren<Text>()[0].text;
        for(int i = 0; i < hero.Spells.Length; i++)
        {
            if(hero.Spells[i] != null && spellName == hero.Spells[i].Name)
            {
                if(hero.Spells[i].IsBuy)
                {
                   
                }
                if (hero.Spells[i].IsEquiped)
                {
                    hero.Spells[i].IsEquiped = false;
                    CheckEquiped(i, hero.Spells[i]);
                }
                else
                {
                    if (NumberOfSpellsEquiped() < 4)
                    {
                        hero.Spells[i].IsEquiped = true;
                        CheckEquiped(i, hero.Spells[i]);
                    }

                }

                break;
            }
        }

    }

    private int NumberOfSpellsEquiped()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;
        int nb = 0;
        foreach(Spells spell in hero.Spells)
        {
            if(spell != null)
            {
                if (spell.IsEquiped)
                    nb++;
            }
        }

        return nb;
    }

    public static void SetToInactiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.gray;
        button.enabled = false;
    }
    public static void SetToActiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.white;
        button.enabled = true;
    }

}
