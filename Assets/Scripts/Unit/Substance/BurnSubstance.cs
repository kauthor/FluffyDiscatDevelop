using UnityEditor;
using UnityEngine;

namespace FluffyDisket.Substance
{
    public class BurnSubstance:BaseSubstance, ISubstateStackable
    {
        public override SubstanceType tpye => SubstanceType.Burn;

        private float damageDuration;
        
        public override void Start()
        {
            base.Start();
            CurrentStack = 1;
        }

        protected override void OnExecute()
        {
            //throw new System.NotImplementedException();
            
            damageDuration += Time.deltaTime;
            if (damageDuration >= 2)
            {
                //데미지 주는 부분
                var maxHp = Owner.MaxHp;
                float damageAmount = 0;

                if (CurrentStack == 1)
                    damageAmount = 0.05f;
                else if (CurrentStack == 2)
                    damageAmount = 0.15f;
                else damageAmount = 0.35f;
                Owner.SetHp(-(damageAmount * maxHp));
                damageDuration = 0;
            }
        }

        public int MaxStack => 3;
        public int CurrentStack { get; private set; }
        public void StackThis()
        {
            //throw new System.NotImplementedException();
            if (CurrentStack >= MaxStack)
                return;

            CurrentStack++;
        }
    }
}