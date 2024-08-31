using System.Reflection;

namespace Stepwise;

public class StepManager
{
    private static IList<Type> _steps;
    private static IList<Assembly> _assemblyList = [Assembly.GetExecutingAssembly()];
    private static Type _currentStep;
    private static bool _stayInCurrentStep;
    public static bool IsCompleted { get; set; }

    public static async Task RunAsync()
    {
        while (!IsCompleted)
        {
            await RunStepAsync();
        }
    }

    public static void SetNextStep(int stepOrder, bool forced = false)
    {
        _stayInCurrentStep = forced;
        _currentStep = GetStepByOrder(stepOrder);
    }

    public static void SetNextStep<T>(bool forced = false) where T : Step
    {
        _stayInCurrentStep = forced;
        _currentStep = typeof(T);
    }

    public static void SetNextStep()
    {
        var steps = GetSteps();

        var current = steps.IndexOf(GetCurrentStep());
        var next = current + 1;

        if (steps.Count > next)
        {
            _currentStep = steps.ElementAt(next);
            return;
        }

        IsCompleted = true;
    }

    public static Type GetCurrentStep()
    {
        return _currentStep ??= GetSteps()?.FirstOrDefault();
    }

    public static async Task RunStepAsync()
    {
        Console.Clear();

        var currentStep = GetCurrentStep();
        if (currentStep == null)
        {
            IsCompleted = true;
            return;
        }

        await ((Step)Activator.CreateInstance(currentStep)!).RunAsync();

        if (_stayInCurrentStep)
        {
            _stayInCurrentStep = false;
            return;
        }

        if (currentStep == GetCurrentStep())
        {
            SetNextStep();
        }
    }

    public static async Task RunStepAsync(Step step)
    {
        await step.RunAsync();
    }

    public static IList<Type> GetSteps()
    {
        var allStepTypes = new List<Type>();

        if (_assemblyList == null || _assemblyList.Count == 0)
        {
            throw new Exception("There is no configured assembly");
        }

        foreach (var assembly in _assemblyList)
        {
            var steps = assembly.GetTypes()
                .Where(x => x.IsClass)
                .Where(x => !x.IsAbstract)
                .Where(x => x.IsSubclassOf(typeof(Step)))
                .Where(x => x.GetCustomAttribute<StepAttribute>()!.Order > 0);

            if (steps.Any())
            {
                allStepTypes.AddRange(steps);
            }
        }

        return _steps ??= [.. allStepTypes.OrderBy(x => x.GetCustomAttribute<StepAttribute>()!.Order)];
    }

    public static void ForceComplete()
    {
        IsCompleted = true;
    }

    private static Type GetStepByOrder(int order)
    {
        return GetSteps().First(x => x.GetCustomAttribute<StepAttribute>()!.Order == order);
    }

    internal static void SetStepFromAssembly(params Assembly[] assemblies)
    {
        _assemblyList ??= [];

        foreach (var assembly in assemblies)
        {
            if (!_assemblyList.Any(x => x == assembly))
            {
                _assemblyList.Add(assembly);
            }
        }
    }
}