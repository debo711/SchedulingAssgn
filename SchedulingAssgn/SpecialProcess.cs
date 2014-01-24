using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    public class SpecialProcess
    {
        private string name;
        private int startPriority;
        private int currentPriority;
        private int timeNeededInCPUBeforeIO;
        private int timeIOTakes;
        private int numABIterations = 0;
        private int currentTimeInCPU = 0;
        private int waitingTimeCurrentIOLeft = 0;
        private int processWaitingTimeInQueue = 0;
        private int totalTimeInCPU = 0;
        private int totalTimeInIO = 0;
        private int totalTimeInQueue = 0;
        private int shortestTimeInQueue;
        private int longestTimeInQueue = 0;


        public SpecialProcess(string NewName, int Priority, int CPUBurst, int IOJobTime, int NumOfTimesSwitching)
        {
            name = NewName;
            startPriority = Priority;
            currentPriority = startPriority;
            timeNeededInCPUBeforeIO = CPUBurst;
            timeIOTakes = IOJobTime;
            waitingTimeCurrentIOLeft = timeIOTakes;
            numABIterations = NumOfTimesSwitching;
        }

        public string GetName()
        {
            return name;
        }
       
        public int StartPriority
        {
            get { return startPriority; }
            set { startPriority = value; }
        }
            
        public int CurrentPriority
        {
            get { return currentPriority; }
            set { currentPriority = value; }
        }

        public int TimeNeededInCPUBeforeIO
        {
            get { return timeNeededInCPUBeforeIO; }
            set { timeNeededInCPUBeforeIO = value; }
        }  

        public int TimeIOTakes
        {
            get { return timeIOTakes; }
            set { timeIOTakes = value; }
        }

        public int NumABIterations
        {
            get { return numABIterations; }
            set { numABIterations = value; }
        }

        public int CurrentTimeInCPU
        {
            get { return currentTimeInCPU; }
            set { currentTimeInCPU = value; }
        }

        public int WaitingTimeCurrentIOLeft
        {
            get { return waitingTimeCurrentIOLeft; }
            set { waitingTimeCurrentIOLeft = value; }
        }
        public int ProcessWaitingTimeInQueu
        {
            get { return processWaitingTimeInQueue; }
            set { processWaitingTimeInQueue = value; }
        }
        public int TotalTimeInCPU
        {
            get { return totalTimeInCPU; }
            set { totalTimeInCPU = value; }
        }
        public int TotalTimeInIO
        {
            get { return totalTimeInIO; }
            set { totalTimeInIO = value; }
        }
        public int TotalTimeInQueue
        {
            get { return totalTimeInQueue; }
            set { totalTimeInQueue = value; }
        }

        public int ShortestTimeInQueue
        {
            get { return shortestTimeInQueue; }
            set { FindShortestTime(value); }
        }

        private void FindShortestTime(int NewTime)
        {
            if (NewTime < shortestTimeInQueue)
                shortestTimeInQueue = NewTime;
        }

        public int LongestTimeInQueue
        {
            get { return longestTimeInQueue; }
            set { FindLongestTime(value); }
        }

        private void FindLongestTime(int NewTime)
        {
            if (NewTime > longestTimeInQueue)
                longestTimeInQueue = NewTime;
        }

    }
}
