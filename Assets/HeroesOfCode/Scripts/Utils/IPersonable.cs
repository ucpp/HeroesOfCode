namespace Maryan.HeroesOfCode
{
    public interface IPersonable<T> where T : class
    {
        T Owner { get; set; }
    }
}