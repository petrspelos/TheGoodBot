﻿using System.Threading.Tasks;
using TheGoodBot.Core;

namespace TheGoodBot
{
    class Program
    {
        static void Main(string[] args) => new BasicBotClient().InitializeAsync().GetAwaiter().GetResult();
    }
}