﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NBeeNET.Mjolnir.Storage.Core.Interface;
using NBeeNET.Mjolnir.Storage.Core.Models;

namespace NBeeNET.Mjolnir.Storage.Core.Implement
{
    public class Scheduler : IScheduler
    {
        private Queue<IJobExecutionContext> queues = null;
        public Scheduler()
        {
            queues = new Queue<IJobExecutionContext>();
        }

        public Scheduler(string schedulerName)
        {
            SchedulerName = schedulerName;
            queues = new Queue<IJobExecutionContext>();
        }

        public string SchedulerName { get; set; }

        public void AddJob(IJobExecutionContext jobContext)
        {
           queues.Enqueue(jobContext);
        }

        public void Clear()
        {
             queues.Clear(); 
        }

        public IJobDetail GetJobDetail(string jobKey)
        {
           return queues.ToList().Where(t => t.JobDetail.Key == jobKey)?.First().JobDetail; 
        }

        public IEnumerable<string> GetJobKeys(string matcher = "")
        {
            return queues.ToList().Where(t => t.JobDetail.Key == matcher).Select(t => t.JobDetail.Key); 
        }

        /// <summary>
        /// Job上下文
        /// </summary>
        public List<IJobExecutionContext> JobContextList { get; set; } = new List<IJobExecutionContext>();

        public async Task Start()
        {
            if (queues.Count > 0)
            {
                IJobExecutionContext jobContext = null;

                //循环作业
                while (queues.Count > 0 && (jobContext = queues.Dequeue()) != null)
                {
                    this.JobContextList.Add(jobContext);
                    await ((IJob)jobContext.JobInstance).Execute(jobContext);

                    //if (jobContext.Result!=null)
                    //{
                    //   var tempJsonPath = jobContext.JobDetail.JobDataMap["tempJsonPath"].ToString();
                    //   JsonFile JsonFileModel = JsonFile.ReadFrom(tempJsonPath);
                    //   var result = (JsonFileDetail)jobContext.Result;
                    //}
                }

            }
           
        }
    }
}
