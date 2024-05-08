namespace FluffyDisket.Trait
{
    public class InstantKillExecuter:SkillOptionExecuter
    {
        //override 
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