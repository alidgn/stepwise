namespace Stepwise;

[AttributeUsage(AttributeTargets.Class)]
public class StepAttribute : Attribute
{
    /// <summary>
    /// This defines the order in which the step is executed. If not specified or specifying zero, the step doesn't include in the step pipeline.
    /// </summary>
    public uint Order { get; set; }
    public string Description { get; set; }

    public StepAttribute()
    {
    }
}