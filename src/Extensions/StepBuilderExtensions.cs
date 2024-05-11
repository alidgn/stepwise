using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Stepwise.Extensions
{
    public static class StepBuilderExtensions
    {
        public static IServiceCollection AddStepwise(this IServiceCollection serviceCollection, Assembly defaultAssembly, params Assembly[] assemblies)
        {
            if (defaultAssembly == null)
            {
                throw new ArgumentNullException(nameof(defaultAssembly));
            }

            StepManager.SetStepFromAssembly(defaultAssembly);

            if (assemblies.Length > 0)
            {
                StepManager.SetStepFromAssembly(assemblies);
            }

            var steps = StepManager.GetSteps();
            foreach (var step in steps)
            {
                serviceCollection.AddSingleton(step.GetTypeInfo());
            }

            return serviceCollection;
        }
    }
}
