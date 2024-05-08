﻿using Tables;

namespace FluffyDisket.Trait
{
    public class SkillOptionExecuter
    {
        public OptionType option => (OptionType)optionData.battleOptionType;
        public virtual void Execute() {}
        public BattleEventParam param;

        public TraitBase trait;
        public TraitOptionData optionData;
    }
}