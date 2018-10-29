namespace Maryan.HeroesOfCode
{
    public interface ITargetable<T> where T : class
    {
        T Target { get; set; }
    }
}