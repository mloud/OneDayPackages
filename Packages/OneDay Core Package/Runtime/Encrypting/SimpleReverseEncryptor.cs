using System;

namespace OneDay.Core
{
    public class SimpleReverseEncryptor : IEncryptor
    {
        public string Name => "reverse";
        private int BytesToReverseCount { get; }

        public SimpleReverseEncryptor(int bytesToReverseCount) =>
            BytesToReverseCount = bytesToReverseCount;
      
        public void Encrypt(byte[] data)
        {
            var bytesToReverse = Math.Min(data.Length, BytesToReverseCount);
            Array.Reverse(data, 0, bytesToReverse);
        }

        public void Decrypt(byte[] data)
        {
            var bytesToReverse = Math.Min(data.Length, BytesToReverseCount);
            Array.Reverse(data, 0, bytesToReverse);
        }
    }
}