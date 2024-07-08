using H.Necessaire;

namespace H.Benchmarks.CLI
{
    internal class CLIDependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            /*
            dependencyRegistry
                .Register<Logging.DependencyGroup>(() => new Logging.DependencyGroup())
                ;
            */
        }
    }
}
