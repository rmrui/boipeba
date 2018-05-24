using System;
using System.Security.Cryptography;
using System.Text;
using Boipeba.Core.Domain.Services;

namespace Boipeba.Core.Infra.Services
{
    /// <summary>
    /// Serviço de criptografia.
    /// </summary>
    public interface IEncryptionService: IService
    {
        /// <summary>
        /// Criptografar valor numérico.
        /// </summary>
        string Encrypt(long content);

        /// <summary>
        /// Criptografar valor literal.
        /// </summary>
        string Encrypt(string content);

        /// <summary>
        /// Descriptografar valor numérico.
        /// </summary>
        long DecryptLong(string content);

        /// <summary>
        /// Descriptografar valor literal.
        /// </summary>
        string Decrypt(string content);

        /// <summary>
        /// Transforma hash md5
        /// </summary>
        string CreateHash(string text);
    }

    /// <summary>
    /// Serviço de criptografia.
    /// </summary>
    public class TripleDESEncryptionService : IEncryptionService
    {
        private readonly byte[] _key = new byte[] { 11, 250, 01, 6, 16, 18, 91, 7, 91, 94, 250, 11, 250, 159, 149, 01, 6, 16, 18, 91, 7, 91, 94, 250 };

        /// <summary>
        /// Criptografar valor numérico.
        /// </summary>
        public string Encrypt(long content)
        {
            return Encrypt(content.ToString());
        }

        /// <summary>
        /// Criptografar valor literal.
        /// </summary>
        public string Encrypt(string content)
        {
            ICryptoTransform transform = CreateTranformer(true);

            UTF8Encoding enc = new UTF8Encoding(false);

            byte[] source = enc.GetBytes(content);

            byte[] cipherText = transform.TransformFinalBlock(source, 0, source.Length);

            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Descriptografar valor numérico.
        /// </summary>
        public long DecryptLong(string content)
        {
            return long.Parse(Decrypt(content));
        }

        /// <summary>
        /// Descriptografar valor literal.
        /// </summary>
        public string Decrypt(string content)
        {
            ICryptoTransform transform = CreateTranformer(false);

            UTF8Encoding enc = new UTF8Encoding(false);

            byte[] source = Convert.FromBase64String(content);

            byte[] cipherText = transform.TransformFinalBlock(source, 0, source.Length);

            return enc.GetString(cipherText);
        }

        /// <summary>
        /// Transforma hash md5
        /// </summary>
        public string CreateHash(string text)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private ICryptoTransform CreateTranformer(bool forEncryption)
        {
            byte[] key = null;
            byte[] pdbsalt = null;
            byte[] iv = null;

            try
            {
                // Salt byte array.
                pdbsalt = GenerateSalt();

                // Create PasswordDeriveBytes object that will generate
                // a Key for TripleDES algorithm.
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(_key, pdbsalt);

                iv = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                // Create a private key for TripleDES algorithm.
                // The iv parameter is not currently used.
                // * http://blogs.msdn.com/shawnfa/archive/2004/04/14/113514.aspx
                key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, iv);

                if (forEncryption)
                {
                    return TripleDES.Create().CreateEncryptor(key, iv);
                }
                else
                {
                    return TripleDES.Create().CreateDecryptor(key, iv);
                }
            }
            catch (CryptographicException)
            {
                return null;
            }
            finally
            {
                if (key != null)
                {
                    Array.Clear(key, 0, key.Length);
                }
                if (pdbsalt != null)
                {
                    Array.Clear(pdbsalt, 0, pdbsalt.Length);
                }
                if (iv != null)
                {
                    Array.Clear(iv, 0, iv.Length);
                }
            }
        }

        /// <summary>
        /// Creates a random salt vector.
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

            // Byte array of the same size as SHA1 hash, which is 160 bits.
            // Would PasswordDeriveBytes benefit from a larger size salt array?
            byte[] arrRandom = new byte[20];
            // Fill the array with random values.
            Gen.GetBytes(arrRandom);
            return arrRandom;
        }
    }
}