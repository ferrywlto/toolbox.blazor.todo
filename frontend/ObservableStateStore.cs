public abstract class ObservableStateStore<TState> 
: IObservable<TState>, IDisposable where TState : new()
{
    protected HashSet<IObserver<TState>> Observers = [];
    protected virtual TState State { get; set; } = new TState();
    // protected readonly Dictionary<string, Action<object>> Mutations = [];     
    // protected readonly Dictionary<string, Func<object?, Task<object>>> Actions = [];
    
    // protected void Commit(string name, object payload)
    // {
    //     if (Mutations.TryGetValue(name, out Action<object>? mutation) && State != null)
    //     {
    //         lock(State)  
    //         {
    //             mutation(payload);
    //         }   
    //         StateHasChanged();
    //     }
    // }
    // public async Task<object> Dispatch(string name, object? payload = null) 
    // {
    //     if (Actions.TryGetValue(name, out Func<object?, Task<object>>? action))
    //     {
    //         await action(payload);
    //     }
    //     return Task.CompletedTask;
    // }
    public virtual IDisposable Subscribe(IObserver<TState> observer)
    {
        Observers.Add(observer);
        return this;
    }
    public virtual void Unsubscribe(IObserver<TState> observer) 
    {
        Observers.Remove(observer);
    }
    public virtual void StateHasChanged()
    {
        foreach (var observer in Observers)
        {
            observer.OnNext(State);
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Observers.Clear();
        // Actions.Clear();
        // Mutations.Clear();
    }
}
