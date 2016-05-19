using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S_M_D.Character;

namespace Assets.Scripts.Combat.heros
{
    public class HeroAndMonsterSprite
    {
        public int chooseHeroSprite(BaseHeros hero)
        {
            int result;
            if (hero.CharacterClassName == "Mage")
            {
                if (hero.IsMale) result = 5;
                else result = 0;
            }
            else if (hero.CharacterClassName == "Warrior")
            {
                if (hero.IsMale) result = 6;
                else result = 2;
            }
            else if (hero.CharacterClassName == "Paladin")
            {
                if (hero.IsMale) result = 4;
                else result = 3;
            }
            else
            {
                if (hero.IsMale) result = 1;
                else result = 7;
            }                

            return result;
        }

        internal int chooseMonsterSprite(BaseMonster monster)
        {
            int result;
            if (monster.Type.ToString() == "ORC") result = 0;
            else if (monster.Type.ToString() == "GOBELIN") result = 1;
            else if (monster.Type.ToString() == "TROLL") result = 2;
            else result = 3;
            return result;
        }
    }
}
