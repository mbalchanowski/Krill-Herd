using System;

namespace KrillHerd
{
    public class RunEventArgs : EventArgs
    {
        public int Iteration { get; set; }

        public RunEventArgs(int iteration)
        {
            Iteration = iteration;
        }
    }
}
