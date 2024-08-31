namespace Stepwise;

[AttributeUsage(AttributeTargets.Class)]
public class StepAttribute : Attribute
{
    /// <summary>
    /// This defines the order in which the step is executed. If not specified, step doesn't include in step pipeline.
    /// </summary>
    public int? Order { get; set; }
    public string Description { get; set; }

    public StepAttribute()
    {
    }
}