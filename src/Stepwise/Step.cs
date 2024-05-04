namespace Stepwise;

public class Step
{
    public virtual async Task RunAsync()
    {
        await Task.CompletedTask;
    }
}