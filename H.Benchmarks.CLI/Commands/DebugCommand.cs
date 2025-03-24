using H.Benchmarks.CLI.BLL;
using H.Necessaire;
using H.Necessaire.Runtime.CLI.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.Commands
{
    internal class DebugCommand : CommandBase
    {
        public override async Task<OperationResult> Run()
        {
            Log("Debugging...");
            using (new TimeMeasurement(x => Log($"DONE Debugging in {x}")))
            {
                await Task.Delay(TimeSpan.Zero);

                var data = DataGenerator.NewRandomIntsEnumerable().ToArray();
            }

            return OperationResult.Win();
        }
    }
}
