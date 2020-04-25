﻿using System;
namespace Hobgoblin
{

    public static class Globals
    {
        public const int weaponMaxAmount = 10;
        public const int potionMaxAmount = 20;
        public const int maxCommandsPerKey = 1;
        public readonly static Random random = new Random((int)DateTime.Now.Ticks);
    }
}
