using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingAssgn
{
    class IOQueu
    {
        private List<SpecialProcess> Queue = new List<SpecialProcess>();

        public bool IsEmpty()
        {
            if (Queue.Count() != 0)
                return false;
            else
                return true;
        }

        public void Encue(SpecialProcess Process)
        {
            Queue.Add(Process);
            Console.WriteLine("\nProcess " + Process.GetName() + " added to IO queue");
        }

        public void Decue(SpecialProcess Process)
        {
            Queue.Remove(Process);
            Console.WriteLine("\nProcess " + Process.GetName() + " is done in IO");
        }

        public int Length 
        {
            get { return Queue.Count;}
        }

        public SpecialProcess GetProcessFromIOList()
        {
            return Queue[0];
        }
    }
}
