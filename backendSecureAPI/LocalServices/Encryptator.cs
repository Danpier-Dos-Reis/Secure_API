using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LocalServices {
  public class Encryptator {
    public string EncriptarAES(string rdnKey, string username) {
      byte[] iv = new byte[16];
      byte[] array;

      using(Aes aes = Aes.Create()) {
        byte[] keyBytes = new byte[32];
        byte[] passwordBytes = Encoding.UTF8.GetBytes(username);

        /*Math.Min() function is used to return the smaller of two numbers.*/
        Array.Copy(passwordBytes, keyBytes, Math.Min(passwordBytes.Length, keyBytes.Length));

        aes.Key = keyBytes;
        aes.IV = iv; /*IV(Initianilization Vector)*/

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        /*MemoryStream class in C# is used to create a stream [whose backing store](cuyo lugar de almacen) is memory.
        When you create an instance of MemoryStream, you are effectively creating a memory buffer(A buffer is a temporary
        storage area in computer memory used to hold data while it is being transferred from one place to another.)
        that can be used to store data temporarily in RAM.*/
        using(MemoryStream memoryStream = new MemoryStream()) //=>Open a memory buffer
        {
          //target a buffer to work and save in the buffer a cryptostream object
          using(CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write)) {
            //use te cryptostream object to write in the buffer a "username" related with "rdnKey" param
            using(StreamWriter streamWriter = new StreamWriter((Stream) cryptoStream)) {
              streamWriter.Write(rdnKey);
            }

            array = memoryStream.ToArray();
          }
        }
      }
      //return "rdnKey" encrypted
      return Convert.ToBase64String(array);
    }

    public string DesencriptarAES(string encryptedKey, string username)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(encryptedKey);

        using (Aes aes = Aes.Create())
        {
            // Crear una username de 256 bits (32 bytes) a partir de la username proporcionada
            byte[] keyBytes = new byte[32];
            byte[] passwordBytes = Encoding.UTF8.GetBytes(username);
            Array.Copy(passwordBytes, keyBytes, Math.Min(passwordBytes.Length, keyBytes.Length));

            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            //place in a buffer the encryoted message
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {   //target a buffer to work and save in the buffer a cryptostream object
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {   //return desencrypted message
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
  }
}