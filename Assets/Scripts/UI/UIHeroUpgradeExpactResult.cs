using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroUpgradeExpactResult:UIMonoBehaviour
    {
        [SerializeField] private Text txtMaxHpBefore;
        [SerializeField] private Text txtMaxHpAfter;
        
        [SerializeField] private Text txtAttackBefore;
        [SerializeField] private Text txtAttackAfter;
        
        [SerializeField] private Text txtPDBefore;
        [SerializeField] private Text txtPDAfter;
        
        [SerializeField] private Text txtMDBefore;
        [SerializeField] private Text txtMDAfter;


        public void InitData(CharacterData data)
        {
            var trainAmount = AccountManager.GetInstance().GetTrainedAmount(data.id);

            int hpdelta = 
                Mathf.Max(ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(14).data, 3);

            int atkdelta = 20;
               // Mathf.Max(ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(15).data, 20);

            int pddelta = 8;
                //Mathf.Max(ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(16).data, 8);

            int mddelta = 8;
                //Mathf.Max(ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(17).data, 8);
            
            var hpb = data.maxHp + hpdelta*trainAmount;
            var hpa = hpb + hpdelta;

            var atb = data.atk + atkdelta * trainAmount;
            var ata = atb + atkdelta;

            var pdb = data.armor + pddelta * trainAmount;
            var pda = pdb + pddelta;

            var mdb = data.magicArmor + mddelta * trainAmount;
            var mda = mdb + mddelta;

            txtMaxHpBefore.text = hpb.ToString();
            txtMaxHpAfter.text = hpa.ToString();

            txtAttackBefore.text = atb.ToString();
            txtAttackAfter.text = ata.ToString();
            
            txtPDBefore.text = pdb.ToString();
            txtPDAfter.text = pda.ToString();
            
            txtMDBefore.text = mdb.ToString();
            txtMDAfter.text = mda.ToString();
        }
        
        
    }
}