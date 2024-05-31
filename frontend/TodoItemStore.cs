public class TodoItemStore : ObservableStateStore<List<TodoItem>>
{
    protected override List<TodoItem> State { get; set; } = [];
    protected bool Initialized = false;
    private readonly BackendAPIGateway backendAPIGateway;
    
    public TodoItemStore(BackendAPIGateway backendAPIGateway)
    {
        this.backendAPIGateway = backendAPIGateway;
    }

    public List<TodoItem> GetAll() => State;
    public async Task ReloadAll(bool forceReload = false)
    {
        if (!Initialized || forceReload)
        {
            var todoItems = await backendAPIGateway.GetTodoItems();
            State = todoItems;
            Initialized = true;
            StateHasChanged();
        }
    }
    public void UpdateIsComplete(int id, bool isComplete)
    {
        var itemToUpdate = State.FirstOrDefault(item => item.Id == id);
        if (itemToUpdate != null)
        {
            State.Remove(itemToUpdate);
            var updatedItem = itemToUpdate with { IsComplete = isComplete };
            State.Add(updatedItem);
        }
        State.Sort((a, b) => a.Id.CompareTo(b.Id));
        StateHasChanged();
    }
    public void Delete(int id)
    {
        var itemToDelete = State.FirstOrDefault(item => item.Id == id);
        if (itemToDelete != null)
            State.Remove(itemToDelete);
        StateHasChanged();
    }
    public void Create(string Title, DateOnly? DueDate)
    {
        var maxId = State.Max(todo => todo.Id);
        var newItem = new TodoItem(maxId + 1, Title, DueDate == default ? null : DueDate);
        State.Add(newItem);
        StateHasChanged();
    }
}