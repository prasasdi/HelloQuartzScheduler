// See https://aka.ms/new-console-template for more information
using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

Console.WriteLine("Hello, World!");

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((c, s) =>
    {
        s.AddQuartz(q =>
        {

        });
        s.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });

        s.AddSingleton<IJobScheduler, JobScheduler>();
    }).Build();

var a = builder.Services.GetRequiredService<IJobScheduler>();

await a.RunScheduler();

await builder.RunAsync();