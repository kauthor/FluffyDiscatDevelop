using UnityEngine;

namespace FluffyDisket.Substance
{
    public class PoisonSubstance:BaseSubstance
    {
        public override SubstanceType tpye => SubstanceType.Poison;
        //public override EffectOptionType EffectOptionType { get; }

        private float damageDuration;

        public override void Start()
        {
            base.Start();
            damageDuration = 0;
        }

        protected override void OnExecute()
        {
            damageDuration += Time.deltaTime;
            if (damageDuration >= 1)
            {
                //데미지 주는 부분
                damageDuration = 0;
            }
        }
    }
}