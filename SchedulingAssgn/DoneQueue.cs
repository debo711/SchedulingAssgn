using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    class DoneQueue
    {
        private List<SpecialProcess> Queue = new List<SpecialProcess>();

        public void Encue(SpecialProcess Process)
        {
            Queue.Add(Process);
        }

        public void ShowProcesses()
        {
            Console.WriteLine("\nPROCESSES THAT ARE DONE");
            foreach (SpecialProcess process in Queue)
            {
                Console.WriteLine("Process: " + process.GetName());
                Console.WriteLine("Start Priority: " + process.StartPriority.ToString() +
                    " | Current Prority: " + process.CurrentPriority.ToString() + "\nTime Needed in CPU: "
                    + process.TimeNeededInCPUBeforeIO.ToString() + " | Time IO Takes: " + process.TimeIOTakes.ToString()
                    + "\nNumber of AB interations: " + process.NumABIterations.ToString() +
                    " | Longest Time In Queue:" + process.LongestTimeInQueue.ToString() + " \nShortest Time in Queue:" +
                    process.ShortestTimeInQueue.ToString() + " | Total Time in CPU:" + process.TotalTimeInCPU.ToString()
                    + "\nTotal Time in IO " + process.TotalTimeInIO.ToString() + " | Total Time in Queue: "
                    + process.TotalTimeInQueue.ToString());
                Console.WriteLine();
            }
        }
    }
}
