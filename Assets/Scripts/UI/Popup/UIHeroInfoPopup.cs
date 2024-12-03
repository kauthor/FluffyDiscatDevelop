using System.Globalization;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroInfoPopup:PopupMonoBehavior
    {
        public static void OpenPopup(Job job, int id)
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.PlayerInfo);
            if (pop is UIHeroInfoPopup heroPop)
            {
                heroPop.Init(job, id);
                heroPop.gameObject.SetActive(true);
            }
        }

        [SerializeField] private Image heroPort;
        [SerializeField] private Text heroHp;
        [SerializeField] private Text heroSpeed;
        [SerializeField] private Text heroRange;
        [SerializeField] private Text heroAtkCool;
        [SerializeField] private Text heroClass;
        [SerializeField] private Text heroPhyDef;
        [SerializeField] private Text heroMagDef;
        [SerializeField] private Text heroCrit;
        [SerializeField] private Text heroCritDam;
        [SerializeField] private Text heroDodge;
        [SerializeField] private Text heroRegen;
        [SerializeField] private Text heroAbs;
        [SerializeField] private Text heroInc;
        [SerializeField] private Text heroDec;
        [SerializeField] private Text heroAOE;
        [SerializeField] private Text heroAcc;
        [SerializeField] private Text heroAtk;

        public void Init(Job job, int id)
        {
            switch (job)
            {
                case Job.Warrior:
                    heroPort.color=Color.red;
                    break;
                case Job.Thief:
                    heroPort.color=Color.yellow;
                    break;
                case Job.Archor:
                    heroPort.color=Color.blue;
                    break;
            }

            var stat = ExcelManager.GetInstance().CharT.GetCharData(id).GetCharacterDataAsStat();
            
            heroHp.text = stat.HpMax.ToString();
            heroSpeed.text = stat.MoveSpeed.ToString();
            heroRange.text = stat.Range.ToString();
            heroAtkCool.text = stat.AttackCoolTime.ToString();
            heroPhyDef.text = stat.phyDef.ToString();
            heroClass.text = job.ToString();
            heroMagDef.text = stat.magDef.ToString();
            heroCrit.text = stat.crit.ToString();
            heroCritDam.text = stat.critDam.ToString();
            heroDodge.text = stat.dodge.ToString();
            heroRegen.text = stat.hpRegen.ToString();
            heroAbs.text = stat.hpAbsolve.ToString();
            heroInc.text = stat.atkIncrease.ToString();
            heroDec.text = stat.damageDecrease.ToString();
            heroAOE.text = stat.AOEArea.ToString();
            heroAcc.text = stat.accuracy.ToString();
            heroAtk.text = stat.Atk.ToString();
        }
    }
}