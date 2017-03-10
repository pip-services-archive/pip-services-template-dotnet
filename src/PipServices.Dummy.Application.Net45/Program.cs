﻿using System;
using System.Threading;

namespace PipServices.Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = (new DummyProcess()).RunAsync(args, CancellationToken.None);
                task.Wait();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
