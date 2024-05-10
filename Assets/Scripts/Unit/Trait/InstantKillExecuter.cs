namespace FluffyDisket.Trait
{
    public class InstantKillExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.InstantKill;

        public override void Execute()
        {
            base.Execute();
            if (param?.target is BattleUnit tar)
            {
                tar.Kill();
            }
        }
    }
}