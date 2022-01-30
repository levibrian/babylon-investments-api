using System;
using System.IO;
using System.Security.Cryptography;
using Babylon.Transactions.Domain.Constants;
using Babylon.Transactions.Shared.Extensions;

namespace Babylon.Transactions.Domain.Cryptography
{
    public interface IAesCipher
    {
        string Encrypt(string textToEncrypt, byte[] iv);

        string Decrypt(string encryptedText, byte[] iv);
    }

    public class AesCipher : IAesCipher
    {
        public string Encrypt(string textToEncrypt, byte[] iv)
        {
            var encryptedHash = EncryptStringToBytes(textToEncrypt, iv);

            var encryptedText = Convert.ToBase64String(encryptedHash);

            return encryptedText;
        }

        public string Decrypt(string encryptedText, byte[] iv)
        {
            var encryptedHash = Convert.FromBase64String(encryptedText);

            var decryptedText = DecryptStringFromBytes(encryptedHash, iv);

            return decryptedText;
        }
        
        private byte[] EncryptStringToBytes(string plainText, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            // Create an AesManaged object
            // with the specified key and IV.
            using var aesAlg = new AesManaged();

            aesAlg.Key = Convert.FromBase64String(CipherVariables.Key);
            aesAlg.IV = iv;

            // Create an encryptor to perform the stream transform.
            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using var msEncrypt = new MemoryStream();

            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                //Write all data to the stream.
                swEncrypt.Write(plainText);
            }

            var encrypted = msEncrypt.ToArray();

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using var aesAlg = new AesManaged();

            aesAlg.Key = CipherVariables.Key.FromStringToByteArray();
            aesAlg.IV = iv;

            // Create a decryptor to perform the stream transform.
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}