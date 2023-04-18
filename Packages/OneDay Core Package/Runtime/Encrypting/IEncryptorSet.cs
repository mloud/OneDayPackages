namespace OneDay.Core
{
    public interface IEncryptorSet
    {
        IEncryptor DefaultEncryptor { get; }
        EncryptorSet RegisterEncryptor(IEncryptor encryptor, bool isDefaultForEncrypting);
        bool UnregisterEncryptor(string name);

        void Encrypt(string encryptorName, byte[] data);
        void Decrypt(string encryptorName, byte[] data);
    }
}