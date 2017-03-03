using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Gametek.Monogame.Util
{
    public static class Rand
    {
        private static Random rnd = new Random();

        private const string chars  = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string nums   = "0123456789";

        public static int Next(int Max)
        {
            return rnd.Next(Max);
        }
        public static int Next(int Min, int Max)
        {
            return rnd.Next(Min, Max);
        }

        public static float Next(float Max)
        {
            return (float)rnd.NextDouble() * Max;
        }

        public static float NextSigned(float Max)
        {
            return Signed() * Next(Max);
        }

        public static Vector2 NextVector2(float Max)
        {
            return new Vector2(Rand.Next(Max), Rand.Next(Max));
        }

        public static int Signed()
        {
            return (Rand.Next(0, 2) == 1) ? 1 : -1;
        }

        public static string GetChars(int Length)
        {
            return new string(Enumerable.Repeat(chars, Length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string GetNumbers(int Length)
        {
            return new string(Enumerable.Repeat(nums, Length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string GetRandomChars(int Max)
        {
            int Length = Next(1, Max + 1);

            return new string(Enumerable.Repeat(chars, Length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string GetRandomNumbers(int Max)
        {
            int Length = Next(1, Max + 1);

            return new string(Enumerable.Repeat(nums, Length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
