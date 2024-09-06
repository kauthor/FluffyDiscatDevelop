using FluffyDisket.Trait;
using Tables;
using UnityEngine;
using UnityEditor;

namespace FluffyDisket
{
#if UNITY_EDITOR
    public class UnitCommandTester:MonoBehaviour
    {
        public bool casterIsPlayer;
        public int casterNumber;
        
        //[]
        
        [SerializeField] public bool targetIsPlayer;
        [SerializeField] public int targetNumber;

        public OptionType testOption;
        
        [SerializeField] public int traitId;
        [SerializeField] public int opValue;
        [SerializeField] public int opDuration;

        //[]
        public void AddTestTrait()
        {
            var target = BattleManager.GetInstance().GetEnemy(targetIsPlayer);
            if (targetNumber > target.members.Length)
                return;
            var targetMem = target.members[targetNumber - 1];


            var trait = ExcelManager.GetInstance().TraitT.GetTraitDataById(this.traitId);

            var newTrait = new TraitBase();
            newTrait.Init(targetMem, trait);
            //targetMem.substanceInfo.SetSubstance();
        }

        public void AddTestOption()
        {
            var target = BattleManager.GetInstance().GetEnemy(targetIsPlayer);
            if (targetNumber > target.members.Length)
                return;
            var targetMem = target.members[targetNumber - 1];
            
            var caster = BattleManager.GetInstance().GetEnemy(casterIsPlayer);
            if (casterNumber > caster.members.Length)
                return;
            var cast = target.members[targetNumber - 1];
            
            TraitBase.OptionInvokeEditor(testOption, cast, targetMem);
        }

        public void PauseGame() => Time.timeScale = 0;
        public void ResumeGame() => Time.timeScale = 1;
    }
#endif
}