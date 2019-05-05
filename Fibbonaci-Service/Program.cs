﻿using EasyNetQ;
using Messages;
using System;
using System.Threading.Tasks;

namespace Fibbonaci_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting...");
            var connectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTION_STRING");

            using (var service = new Service(connectionString))
            {
                service.Init();
                service.WaitForCompletion();
            }
        }
    }
}