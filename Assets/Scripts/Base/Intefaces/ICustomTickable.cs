namespace GameSystem
{
    public interface ICustomTickable
    {
        public virtual void OnTick() { }
        public virtual void OnFixedTick() { }
    }
}