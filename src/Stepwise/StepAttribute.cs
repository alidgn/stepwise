namespace Stepwise;

[AttributeUsage(AttributeTargets.Class)]
public class StepAttribute : Attribute
{
    public int Order { get; set; } = int.MaxValue;
    public string Description { get; set; }

    public StepAttribute()
    {
    }
}