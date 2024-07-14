using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIBattleUnitInfoParts:UIMonoBehaviour
    {
        private BattleUnit Owner;

        [SerializeField] private Image hpBar;
        [SerializeField] private Text txtAtk;
        [SerializeField] private Text txtDef;
        [SerializeField] private Text txtMDef;
        [SerializeField] private Text txtMaxHp;
        [SerializeField] private Text txtCurrentHp;
        [SerializeField] private Text Klasse;

        
        [SerializeField] private Image imgFace;

        public void Init(BattleUnit owner)
        {
            Owner = owner;
            Owner.onHpUpdate -= OnHpUpdate;
            Owner.onHpUpdate += OnHpUpdate;
            txtMaxHp.text = Owner.MaxHp.ToString(CultureInfo.InvariantCulture);
            txtCurrentHp.text = Owner.MaxHp.ToString(CultureInfo.InvariantCulture);
            txtAtk.text = owner.AbilityDatas.Atk.ToString();
            txtDef.text = owner.AbilityDatas.phyDef.ToString();
            txtMDef.text = owner.AbilityDatas.magDef.ToString();

            Klasse.text = Owner.CharacterClassPublic.ToString();
        }
        
        private void OnHpUpdate(float per)
        {
            hpBar.fillAmount = per;
            txtCurrentHp.text = Owner.currentHp.ToString(CultureInfo.InvariantCulture);
        }
    }
}