
using Common.Interfaces;
using System;
using System.Text;

namespace Common.IdGenerator
{
    public class RandomUrlSafeIdGenerator : IIdGenerator
    {
        private const int idLength = 7;
        private const string CharacterPool =
            @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$-_.+!*'(),";
        private Random rand;

        public RandomUrlSafeIdGenerator()
        {
            rand = new Random();
        }

        public string GenerateId()
        {
            return Generate(idLength);
        }

        private string Generate(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException(nameof(length));
            }

            StringBuilder idBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int idx = rand.Next(0, CharacterPool.Length);
                idBuilder.Append(CharacterPool[idx]);
            }
            return idBuilder.ToString();
        }
    }
}
