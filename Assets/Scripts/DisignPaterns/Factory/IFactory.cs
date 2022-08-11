public interface IFactory<T>
{
    T Create();
}
public interface IFactory<T,P>
{
    T Create(P parameter);
}
