using Tables;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class HeroStatDivideComponent:MonoBehaviour
    {
        [SerializeField] private Image heroProfile;
        [SerializeField] private StatDividePart[] statParts;
        [SerializeField] private Text levelUpStat;

        private int maxStatToDivide;
        private int current;
        private int playerID;
        
        public void Init(int playerId, int stat)
        {
            maxStatToDivide = stat;
            current = 0;
            playerID = playerId;

            var baseStatMulti = ExcelManager.GetInstance().BaseT;
            var baseStat = ExcelManager.GetInstance().CharT.GetCharData(playerId);
            var levelStat = AccountManager.GetInstance().CharacterLevelupStats[playerId];
            statParts[0].Init(baseStatMulti.GetBaseDataByIndex(23).data, PlusClicked, MinusClicked, 
                baseStat.maxHp + levelStat.hp * baseStatMulti.GetBaseDataByIndex(23).data);
            statParts[1].Init(baseStatMulti.GetBaseDataByIndex(24).data, PlusClicked, MinusClicked, 
                baseStat.atk + levelStat.atk * baseStatMulti.GetBaseDataByIndex(24).data);
            statParts[2].Init(baseStatMulti.GetBaseDataByIndex(25).data, PlusClicked, MinusClicked, 
                baseStat.armor + levelStat.phyd * baseStatMulti.GetBaseDataByIndex(25).data);
            statParts[3].Init(baseStatMulti.GetBaseDataByIndex(26).data, PlusClicked, MinusClicked, 
                baseStat.magicArmor + levelStat.magd * baseStatMulti.GetBaseDataByIndex(26).data);

            levelUpStat.text = stat.ToString();
        }


        private void MinusClicked()
        {
            current--;
            foreach (var p in statParts)
            {
                p.UpdateComponents(true);
            }
            levelUpStat.text = (maxStatToDivide-current).ToString();
        }

        private void PlusClicked()
        {
            current++;
            foreach (var p in statParts)
            {
                p.UpdateComponents(current<maxStatToDivide);
            }
            levelUpStat.text = (maxStatToDivide-current).ToString();
        }

        public bool DivideCompleted() => current >= maxStatToDivide;


        public void TryAddStatToPlayer()
        {
            AccountManager.GetInstance().AddPlayerLevelUpStat(playerID, 
                statParts[1].CurrentSigned,
                statParts[0].CurrentSigned
                ,statParts[2].CurrentSigned
                ,statParts[3].CurrentSigned);
        }
    }
}