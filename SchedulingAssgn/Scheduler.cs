using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SchedulingAssgn
{
    class Scheduler
    {
        static void Main(string[] args)
        {

            ReadyQueue ProcessQueue = new ReadyQueue();
            DoneQueue ProcessesDone = new DoneQueue();
            IOQueu ListWatingForIO = new IOQueu();
            IO MyIO = new IO(ListWatingForIO);
            CPU MyCPU = new CPU();

            int IO_JobTime = 3;
            int MAXTIMEINCPU = 5;
            
            //ProcessQueue.Encue(Name,Priority,CPU_Burst,IO_JobTime, times to switch back and forth between CPU and IO)
            ProcessQueue.Encue(new SpecialProcess("P1", 5, 8, IO_JobTime, 8));
            ProcessQueue.Encue(new SpecialProcess("P2", 7, 4, IO_JobTime, 4));
            ProcessQueue.Encue(new SpecialProcess("P3", 9, 14, IO_JobTime, 16));
            ProcessQueue.Encue(new SpecialProcess("P4", 3, 7, IO_JobTime, 7));
            ProcessQueue.Encue(new SpecialProcess("P5", 11, 3, IO_JobTime, 3));
            ProcessQueue.Encue(new SpecialProcess("P6", 15, 2, IO_JobTime, 2));
            ProcessQueue.Encue(new SpecialProcess("P7", 10, 1, IO_JobTime, 1));
            ProcessQueue.Encue(new SpecialProcess("P8", 2, 11, IO_JobTime, 11));
            ProcessQueue.Encue(new SpecialProcess("P9", 8, 13, IO_JobTime, 13));
            ProcessQueue.Encue(new SpecialProcess("P10", 10, 10, IO_JobTime, 10));
            ProcessQueue.Encue(new SpecialProcess("P11", 6, 5, IO_JobTime, 5));
            ProcessQueue.Encue(new SpecialProcess("P12", 14, 6, IO_JobTime, 6));
            ProcessQueue.Encue(new SpecialProcess("P13", 4, 15, IO_JobTime, 15));
            ProcessQueue.Encue(new SpecialProcess("P14", 1, 12, IO_JobTime, 12));
            ProcessQueue.Encue(new SpecialProcess("P15", 12, 7, IO_JobTime, 7));
            ProcessQueue.Encue(new SpecialProcess("P16", 13, 9, IO_JobTime, 9));

            ProcessQueue.PrintProcessOrder();
            int Time = 0;

            SpecialProcess currentProcess = ProcessQueue.GetProcess();
            SpecialProcess nextProcess = null;
            SpecialProcess processFromIOQueue = null;

            while (ProcessQueue.IsEmpty() == false || Time < 10000)
            {
                SpecialProcess temp;

                if (!MyCPU.IsBusy())
                {
                    MyCPU.AttachTo(currentProcess);
                    ProcessQueue.Decue(currentProcess);
                    Console.WriteLine("\nAttached to process: " + currentProcess.GetName());
                }
                //there is a process in the cpu and nothing in IO 
                else if (MyCPU.IsBusy() && !MyIO.IsBusy() && currentProcess.CurrentTimeInCPU < MAXTIMEINCPU && currentProcess.TotalTimeInCPU < currentProcess.TimeNeededInCPUBeforeIO)
                {
                    MyCPU.DoWorkOn(currentProcess);     
                }
                //there is a process in the cpu and one in IO
                else if (MyCPU.IsBusy() && MyIO.IsBusy() && currentProcess.CurrentTimeInCPU < MAXTIMEINCPU && currentProcess.TotalTimeInCPU < currentProcess.TimeNeededInCPUBeforeIO)
                {
                    MyCPU.DoWorkOn(currentProcess);    //jumps in cpu to work for max time allowed 

                    if (processFromIOQueue.WaitingTimeCurrentIOLeft == 0) //meaning no IO left to do
                    {
                        ListWatingForIO.Decue(processFromIOQueue);
                        MyIO.KickOut(processFromIOQueue);
                        if(processFromIOQueue.TotalTimeInCPU == processFromIOQueue.TimeNeededInCPUBeforeIO) //meaning no CPU to do
                            ProcessesDone.Encue(processFromIOQueue); //send to done queue
                        else
                            ProcessQueue.Encue(processFromIOQueue);  //else send back to ready queue
                    }
                    else
                            MyIO.DoIOFor(processFromIOQueue);
                }
                //the process in the cpu has reached the max time allowed in the cpu
                else if (currentProcess.CurrentTimeInCPU == MAXTIMEINCPU | (currentProcess.TotalTimeInCPU == currentProcess.TimeNeededInCPUBeforeIO && currentProcess.CurrentTimeInCPU == currentProcess.TimeNeededInCPUBeforeIO))
                {
                    MyCPU.KickOut(currentProcess);
                    if (currentProcess.WaitingTimeCurrentIOLeft != 0)  //if there is IO to do
                    {
                        ListWatingForIO.Encue(currentProcess);
                    }
                    else                                        //so kick it back in ready queue
                        ProcessQueue.Encue(currentProcess);

                    if (ListWatingForIO.IsEmpty())
                        Console.WriteLine("No processes in IO queue");
                    else
                    {
                        processFromIOQueue = ListWatingForIO.GetProcessFromIOList();
                        if (processFromIOQueue.WaitingTimeCurrentIOLeft == 0) //process has no IO left to do
                        {
                            ListWatingForIO.Decue(processFromIOQueue);
                            MyIO.KickOut(processFromIOQueue);
                        }
                        else
                            MyIO.DoIOFor(processFromIOQueue);
                    }

                    nextProcess = ProcessQueue.GetProcess();
                    MyCPU.AttachTo(nextProcess);
                    Console.WriteLine("\nCPU got next process in queue: " + nextProcess.GetName());
                    temp = nextProcess;
                    nextProcess = currentProcess;   //current process will become process just gotten from queue
                    currentProcess = temp;
                    ProcessQueue.Decue(currentProcess);
                }
                //jumps in here if process does not need the cpu and IO anymore
                else if (currentProcess.TotalTimeInCPU == currentProcess.TimeNeededInCPUBeforeIO && currentProcess.WaitingTimeCurrentIOLeft == 0)
                {
                    MyCPU.KickOut(currentProcess);
                    ProcessQueue.Decue(currentProcess);
                    Console.WriteLine("\nProcess " + currentProcess.GetName() + " is now DONE");
                    ProcessesDone.Encue(currentProcess);
                    ProcessesDone.ShowProcesses();
                    if (ProcessQueue.IsEmpty() == false)
                    {
                        currentProcess = ProcessQueue.GetProcess();
                        ProcessQueue.Decue(currentProcess);
                    }
                    else if (ProcessQueue.IsEmpty() == true)
                    {
                        break;
                    }
                }

                if(ProcessQueue.IsEmpty() == false)
                    ProcessQueue.TrackWaitTimes();
                Time++;
                if(ProcessQueue.IsEmpty() == false)
                    ProcessQueue.UpdatePriorities(Time);
            }

            Console.WriteLine("Time Units passed: " + Time.ToString());
            Console.ReadLine();
            
        }
    }
}
