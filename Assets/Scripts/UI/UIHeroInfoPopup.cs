using System.Globalization;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroInfoPopup:PopupMonoBehavior
    {
        public static void OpenPopup(Job job, PlayerSubTable table)
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.PlayerInfo);
            if (pop is UIHeroInfoPopup heroPop)
            {
                heroPop.Init(job, table);
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

        public void Init(Job job, PlayerSubTable table)
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

            heroHp.text = table.stat.HpMax.ToString();
            heroSpeed.text = table.stat.MoveSpeed.ToString();
            heroRange.text = table.stat.Range.ToString();
            heroAtkCool.text = table.stat.AttackCoolTime.ToString();
            heroPhyDef.text = table.stat.phyDef.ToString();
            heroClass.text = job.ToString();
            heroMagDef.text = table.stat.magDef.ToString();
            heroCrit.text = table.stat.crit.ToString();
            heroCritDam.text = table.stat.critDam.ToString();
            heroDodge.text = table.stat.dodge.ToString();
            heroRegen.text = table.stat.hpRegen.ToString();
            heroAbs.text = table.stat.hpAbsolve.ToString();
            heroInc.text = table.stat.atkIncrease.ToString();
            heroDec.text = table.stat.damageDecrease.ToString();
            heroAOE.text = table.stat.AOEArea.ToString();
            heroAcc.text = table.stat.accuracy.ToString();
            heroAtk.text = table.stat.Atk.ToString();
        }
    }
}