namespace TooFatGragas.Model
{
    public abstract class ModeBase : Model
    {
        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}