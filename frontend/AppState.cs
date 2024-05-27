public class AppState
{
    private static AppState? self = null;
    public static AppState GetInstance() {
        self ??= new AppState();
        return self;
    }

    public bool Initialized = false;
    public List<TodoItem> todoItems = [];

    public void Clear() { todoItems = []; }
    public List<TodoItem> GetAll() 
    {
        return todoItems;
    }

    public void AddNew(TodoItem todo)
    {
        todoItems.Add(todo);
    }

    public void Delete(int id)
    {
        var todo = todoItems.First(item => item.Id == id);
        todoItems.Remove(todo);
    }

    public void Replace(TodoItem todo)
    {
         
    }
}