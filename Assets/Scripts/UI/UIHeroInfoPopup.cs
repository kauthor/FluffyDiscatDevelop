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
        [SerializeField] private Text heroSkillCool;
        [SerializeField] private Text heroClass;

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
            heroSkillCool.text = table.stat.SkillCoolTIme.ToString();
            heroClass.text = job.ToString();
        }
    }
}