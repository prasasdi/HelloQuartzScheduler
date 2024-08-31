using Core.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IJobScheduler
    {
        Task RunScheduler();
    }
    public class JobScheduler : IJobScheduler
    {
        private readonly ISchedulerFactory _factory;
        private IScheduler _scheduler;

        public JobScheduler(ISchedulerFactory factory)
        {
            _factory = factory;
        }

        // Prepare the scheduler and add jobs and triggers
        public async Task PrepareScheduler()
        {
            // Create and start the scheduler
            _scheduler = await _factory.GetScheduler();
            await _scheduler.Start();
                
            // Create jobs and triggers based on your TriggerModel
            foreach (var jobModel in Trigger.Jobs)
            {
                // Create job
                var job = JobBuilder.Create<ConsoleWriteJob>()
                    .WithIdentity(jobModel.Name)
                    .Build();

                // Create trigger
                var trigger = TriggerBuilder.Create()
                    .ForJob(job)
                    .WithIdentity($"{Trigger.Name}.{jobModel.Name}")
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(1)
                        .RepeatForever())
                    .Build();

                // Assign trigger for job
                // Schedule the job with the trigger
                await _scheduler.ScheduleJob(job, trigger);
            }
        }

        // Run the scheduler
        public async Task RunScheduler()
        {
            if (_scheduler == null)
            {
                await PrepareScheduler();
            }

            // The scheduler is already started in PrepareScheduler
        }

        private TriggerModel Trigger = new TriggerModel()
        {
            Name = "TriggerMessage",
            TriggerType = TriggerTypeEnum.Per5Sec,
            Jobs = new ObservableCollection<JobModel>()
        {
            new JobModel()
            {
                Id = Guid.NewGuid(),
                Name = "PayloadObject",
                JobType = JobTypeEnum.Object
            },
            new JobModel()
            {
                Id = Guid.NewGuid(),
                Name = "PayloadDataType",
                JobType = JobTypeEnum.Datatypes
            },
        }
        };
    }

    public enum GroupType
    {
        Trigger,
        Job
    }
    public abstract class BaseJobModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class JobModel : BaseJobModel 
    {
        public JobTypeEnum JobType { get; set; }
        public TriggerModel Trigger { get; set; }
    }
    public class TriggerModel : BaseJobModel 
    {
        public TriggerTypeEnum TriggerType { get; set; }
        public ObservableCollection<JobModel> Jobs { get; set; } = new ObservableCollection<JobModel>();
    }
    public enum JobTypeEnum
    {
        Object,
        Datatypes,
    }
    public enum TriggerTypeEnum
    {
        Custom,
        Per5Sec,
    }
}
