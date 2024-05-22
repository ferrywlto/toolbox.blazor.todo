namespace Shared;
public class Class1
{

}

public record TodoItem(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);