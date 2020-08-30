namespace CommonLib
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class SimplerAes
    {
        private static readonly byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static readonly byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
        private readonly ICryptoTransform encryptor;
        private readonly ICryptoTransform decryptor;
        private readonly UTF8Encoding encoder;

        public SimplerAes()
        {
            var rm = new RijndaelManaged();
            rm.Key = SimplerAes.key;
            rm.IV = SimplerAes.vector;

            this.encryptor = rm.CreateEncryptor(key, vector);
            this.decryptor = rm.CreateDecryptor(key, vector);
            this.encoder = new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return this.encoder.GetString(this.Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}
