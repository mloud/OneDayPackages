using System;
using System.Collections.Generic;

namespace OneDay.Core
{
    public class EncryptorSet : IEncryptorSet
    {
        public IEncryptor DefaultEncryptor { get; private set; }
        private Dictionary<string, IEncryptor> Encryptors { get; }
        public EncryptorSet() => Encryptors = new();
        
        public EncryptorSet RegisterEncryptor(IEncryptor encryptor, bool isDefaultForEncrypting)
        {
            if (Encryptors.ContainsKey(encryptor.Name))
            {
                throw new ArgumentException($"Encryptor with name {encryptor.Name} is already registered");
            }

            if (isDefaultForEncrypting)
            {
                DefaultEncryptor = encryptor;
            }

            Encryptors.Add(encryptor.Name, encryptor);
            return this;
        }

        public bool UnregisterEncryptor(string name) => Encryptors.Remove(name);

        public void Encrypt(string encryptorName, byte[] data)
        {
            if (Encryptors.TryGetValue(encryptorName, out var encryptor))
            {
                encryptor.Encrypt(data);
            }
            else
            {
                throw new ArgumentException($"Encryptor with name {encryptorName} not registered");
            }
        }

        public void Decrypt(string encryptorName, byte[] data)
        {
            if (Encryptors.TryGetValue(encryptorName, out var encryptor))
            {
                encryptor.Decrypt(data);
            }
            else
            {
                throw new ArgumentException($"Encryptor with name {encryptorName} not registered");
            }
        }
    }
}