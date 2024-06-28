using UnityEngine;

namespace FluffyDisket.Substance
{
    public class BloodSubstance:BaseSubstance
    {
        public override SubstanceType tpye => SubstanceType.Bleed;
        private float damageDuration;
        public override void Start()
        {
            base.Start();
            damageDuration = 0;
        }

        protected override void OnExecute()
        {
            damageDuration += Time.deltaTime;
            if (damageDuration >= 2)
            {
                //데미지 주는 부분
                Owner.SetHp(-value1);
                damageDuration = 0;
            }
        }
    }
}