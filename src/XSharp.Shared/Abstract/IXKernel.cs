namespace XSharp.Shared.Abstract;

public interface IXKernel
{
    T GetInstance<T>();
}