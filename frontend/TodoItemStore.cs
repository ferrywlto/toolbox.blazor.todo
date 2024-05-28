public class TodoItemStore : ApplicationStateStore<TodoItemStore>
{
    protected bool Initialized = false;
    protected List<TodoItem> items = [];
    private readonly BackendAPIGateway backendAPIGateway;
    
    public TodoItemStore(BackendAPIGateway backendAPIGateway)
    {
        this.backendAPIGateway = backendAPIGateway;
    }

    public List<TodoItem> GetAll()
    {
        return items;
    }
    
    public void Mutation_UpdateIsComplete(int id, bool? isComplete)
    {
        var itemToUpdate = items.FirstOrDefault(item => item.Id == id);
        if (itemToUpdate != null)
        {
            items.Remove(itemToUpdate);
            var updatedItem = itemToUpdate with { IsComplete = isComplete ?? false };
            items.Add(updatedItem);
        }
        items.Sort((a, b) => a.Id.CompareTo(b.Id));
        StateHasChanged();
    }
    public void Mutation_Delete(int id)
    {
        var itemToDelete = items.FirstOrDefault(item => item.Id == id);
        if (itemToDelete != null)
            items.Remove(itemToDelete);
        StateHasChanged();
    }
    public void Mutation_Create(string Title, DateOnly? DueDate)
    {
        var maxId = items.Count == 0 ? 0 : items.Max(item => item.Id);
        var newItem = new TodoItem(maxId + 1, Title, DueDate == default ? null : DueDate);
        items.Add(newItem);
        StateHasChanged();
    }
    public void Mutation_ReplaceAll(List<TodoItem> newItems)
    {
        items = newItems;
        StateHasChanged();
    }

    public async Task Action_Reload(bool forceReload = false)
    {
        if (!Initialized || forceReload)
        {
            items = await backendAPIGateway.GetTodoItems();
            Mutation_ReplaceAll(items);
            Initialized = true;
        }
    }
    
    public override void Dispose()
    {
        observers.Clear();
        items.Clear();
    }
}