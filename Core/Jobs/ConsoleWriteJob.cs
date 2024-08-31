using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    /// <summary>
    /// Definisikan apa yang akan dikerjakan
    /// <para>Disini cuman untuk print aja</para>
    /// </summary>
    public class ConsoleWriteJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("ini");
        }
    }
}
