using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _19._09._24_s_p_interlocked._monitor._lock
{
    class Bridge
    {
        private static object Lock = new object();
        private static bool isBridgeOccupied = false;

        public void CrossBridge(int num, string direction)
        {
            lock (Lock)
            {
                while (isBridgeOccupied)
                {
                    Monitor.Wait(Lock);
                }

                isBridgeOccupied = true;
                Console.WriteLine($"{num} car is crossing the bridge from {direction} side.");
                Thread.Sleep(500);
                Console.WriteLine($"Wrrrrrrrrrrrrrrrrrrrrrrrrrr!");
                Thread.Sleep(1000);
                isBridgeOccupied = false;
                Monitor.PulseAll(Lock);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int num_cars = 8;
            Bridge bridge = new Bridge();
            Thread[] cars = new Thread[num_cars];

            for (int i = 0; i < cars.Length; i++)
            {
                int num = i + 1;
                string direction = (i % 2 == 0) ? "left" : "right";
                cars[i] = new Thread(() => bridge.CrossBridge(num, direction));
                cars[i].Start();
            }

            foreach (Thread car in cars)
            {
                car.Join();
            }

            Console.WriteLine("All cars have crossed the bridge.");
        }
    }
}
