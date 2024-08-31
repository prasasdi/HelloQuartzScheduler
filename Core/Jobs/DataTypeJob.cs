using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public enum DataTypes
    {
        String,
        Int,
    }
    public class DataTypeJob : IJob
    {
        public DataTypeJob(DataTypes dataTypes)
        {
            switch (dataTypes)
            {
                case DataTypes.String:
                    break;
                case DataTypes.Int:
                    break;
            }
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("ini");
        }
    }
}
