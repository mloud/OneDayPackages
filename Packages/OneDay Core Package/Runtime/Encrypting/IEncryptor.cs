namespace OneDay.Core
{
    public interface IEncryptor
    {
        public string Name { get; }
        void Encrypt(byte[] data);
        void Decrypt(byte[] data);
    }
}