using Sirenix.OdinInspector;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroInformationPart:UIMonoBehaviour
    {
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtName;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtHp;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtPAtk;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtPDef;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtMdef;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtRange;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtAtkCool;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtSpeed;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtHpRegen;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtAbsolve;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtCrit;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtCritDam;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtDodge;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtDamIncr;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtInhe;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtAoe;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtAcc;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtMoney;
        [FoldoutGroup("Info Text")]
        [SerializeField] private Text txtLuck;


        public void Init(CharacterData data)
        {
            //일단 지금은... 장비 스탯 미적용 시키자
            txtName.text = ((Job) data.job).ToString();
            txtHp.text = data.maxHp.ToString();
            txtPAtk.text = data.atk.ToString();
            txtPDef.text = data.armor.ToString();
            txtMdef.text = data.magicArmor.ToString();
            txtRange.text = data.range.ToString();
            txtAtkCool.text = data.attackCoolTime.ToString();
            txtSpeed.text = data.moveSpeed.ToString();
            txtHpRegen.text = data.hpRegen.ToString();
            txtAbsolve.text = data.hpAbsolve.ToString();
            txtCrit.text = data.critical.ToString();
            txtCritDam.text = data.critDamage.ToString();
            txtDodge.text = data.dodge.ToString();
            txtDamIncr.text = data.damIncrease.ToString();
            txtInhe.text = data.damDecrease.ToString();
            txtAoe.text = data.aeo.ToString();
            txtAcc.text = data.accuracy.ToString();
            txtMoney.text = "0%";
            txtLuck.text = "1";
        }
    }
}