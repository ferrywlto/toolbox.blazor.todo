public abstract class ApplicationStateStore<T> : IObservable<T>, IDisposable
{
    protected List<IObserver<T>> observers = [];
    public abstract void Dispose();
    public virtual IDisposable Subscribe(IObserver<T> observer)
    {
        observers.Add(observer);
        return this;
    }
    public virtual void Unsubscribe(IObserver<T> observer) 
    {
        observers.Remove(observer);
    }
    public virtual void StateHasChanged()
    {
        foreach (var observer in observers)
        {
            observer.OnNext((T)(object)this);
        }
    }
}
