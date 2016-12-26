using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator.ClassDescription;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;
using System.IO;

namespace DTO_Generator
{
    public class DtoGenerator : IDisposable
    {
        private string classNamespace;
        private int maxCountThread;
        public List<GeneratedClass> generatedClassList{ get; private set;}
        private Task[] tasks;
        private ManualResetEvent[] events;
        private int threadPoolSize;
        private ICodeGenerator codeGenerator;
        private int tasksCount;
         
        
        public DtoGenerator(string ClassNamespace, int MaxCountThread)
        {
            this.classNamespace = ClassNamespace;
            this.maxCountThread = MaxCountThread;
            this.codeGenerator = new CodeGenerator();
        }

        public List<GeneratedClass> Generate(ClassList classList)
        {
            if ((classList == null) || (classList.classDescriptions == null))
            {
                throw new InvalidFileException("Check the json file");
            }

            tasksCount = classList.classDescriptions.Length;
            threadPoolSize = Math.Min(tasksCount, maxCountThread);
            CreateAllTasks(tasksCount, classList);
            InitializeThreadPool();
            PerformRemainingTasks();
            generatedClassList = GetGeneratedClasses();
            return generatedClassList;    
        }

        private void CreateAllTasks(int taskCount, ClassList classList)
        {
            tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = new Task(classNamespace, classList.classDescriptions[i]);
            }
        }

        private void InitializeThreadPool()
        {
            events = new ManualResetEvent[threadPoolSize];
            for (int i = 0; i < threadPoolSize; i++)
            {
                events[i] = new ManualResetEvent(false);
                tasks[i].Event = events[i];
                AddToQueue(tasks[i]);
            }
        }

        private void AddToQueue(Task task)
        {
            ThreadPool.QueueUserWorkItem(codeGenerator.GenerateCode, task);
        }

        private void PerformRemainingTasks()
        {
            int indexFreeThread;
            int addedTasks = threadPoolSize;
            while(addedTasks < tasksCount)
            {
                indexFreeThread = WaitHandle.WaitAny(events);
                events[indexFreeThread].Reset();
                tasks[addedTasks].Event = events[indexFreeThread];
                AddToQueue(tasks[addedTasks++]);
            }
            WaitHandle.WaitAll(events);            
        }

        private List<GeneratedClass> GetGeneratedClasses()
        {
            generatedClassList = new List<GeneratedClass>();
            foreach(Task taskItem in tasks)
            {
                generatedClassList.Add(taskItem.GeneratedClass);
            }
            return generatedClassList;
        }

        public void Dispose()
        {
            foreach (ManualResetEvent eventItem in events)
            {
                if (eventItem != null)
                {
                    eventItem.Dispose();
                }
            }
        }
    }
}
