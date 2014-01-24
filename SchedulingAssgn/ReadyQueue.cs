using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    class ReadyQueue
    {
        private List<SpecialProcess> Queue = new List<SpecialProcess>();
        private int process_counter = 0;

        public void Encue(SpecialProcess Process)
        {
            Queue.Add(Process);
            Console.WriteLine(Process.GetName() + " just added to process ready queue\n");

            if (Queue.Count > 1)
            {
                PrioritySort(Queue);
            }
        }

        private void PrioritySort(List<SpecialProcess> QueueOfProcesses)
        {

            for (int i = 0; i < QueueOfProcesses.Count; i++)
            {
                for (int j = 0; j < QueueOfProcesses.Count - 1; j++)
                {
                    if (QueueOfProcesses[j].CurrentPriority > QueueOfProcesses[j + 1].CurrentPriority)
                    {
                        SpecialProcess temp;
                        temp = QueueOfProcesses[j + 1];
                        QueueOfProcesses[j + 1] = QueueOfProcesses[j];
                        QueueOfProcesses[j]= temp;
                    }
                }
            }
        }

        public void Decue(SpecialProcess Process)
        {
            Queue.Remove(Process);
            Process.TotalTimeInQueue = Process.TotalTimeInQueue + Process.ProcessWaitingTimeInQueu;
            Process.ShortestTimeInQueue = Process.ProcessWaitingTimeInQueu;
            Process.LongestTimeInQueue = Process.ProcessWaitingTimeInQueu;
            Process.ProcessWaitingTimeInQueu = 0;
        }

        public bool IsEmpty()
        {
            if (Queue.Count() == 0)
                return true;
            else
                return false;
        }

        public int GetCount()
        {
            return Queue.Count();
        }

        public void PrintProcessOrder()
        {
            foreach (SpecialProcess process in Queue)
            {
                Console.WriteLine(process.GetName() + " Priority:" + process.StartPriority.ToString() + " CPU Burst:" + process.TimeNeededInCPUBeforeIO.ToString());
            }

            Console.ReadLine();
        }

        public SpecialProcess GetProcess()
        {
            Console.WriteLine("\nGETTING NEW PROCESS: " + process_counter++);
            Console.WriteLine("Next Process in QUEUE: " + Queue[0].GetName());
            return Queue[0];
        }

        public void TrackWaitTimes()
        {
            foreach (SpecialProcess process in Queue)
            {
                process.ProcessWaitingTimeInQueu++;
            }
        }

        public void UpdatePriorities(int TimeCounter)
        {
            if ((TimeCounter % 10) == 0) //every 10 time units priority will change so old processes get a chance in the CPU 
            {
                Console.WriteLine("\nUPDATING PRIORITIES");
                foreach (SpecialProcess process in Queue)
                {
                    if (process.CurrentPriority != 1)
                        process.CurrentPriority--;
                    Console.WriteLine("Process " + process.GetName() + " -> StartPrority:" + process.StartPriority.ToString() + " CurrentPriority:" + process.CurrentPriority.ToString()
                        + " TimeInCPU:" + process.TotalTimeInCPU.ToString() + " TimeNeededInIO: " + process.WaitingTimeCurrentIOLeft.ToString());
                }
                Console.WriteLine();
            }
        }

    }
}
