using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SecureAesEncryption
{
    private static readonly string Password = "trongnguyen191098280102"; // Mật khẩu cần bảo mật
    private static readonly int Iterations = 10000; // Số vòng lặp để tăng độ khó
    private static readonly byte[] Salt = Encoding.UTF8.GetBytes("YourFixedSalt1234"); // Salt cố định

    /// <summary>
    /// Tạo Key và IV từ một mật khẩu thông qua PBKDF2.
    /// </summary>
    private static void GenerateKeyAndIV(out byte[] key, out byte[] iv)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(Password, Salt, Iterations))
        {
            key = deriveBytes.GetBytes(32); // 256-bit Key
            iv = deriveBytes.GetBytes(16);  // 128-bit IV
        }
    }

    /// <summary>
    /// Mã hóa chuỗi văn bản bằng AES với Key và IV từ PBKDF2.
    /// </summary>
    public static string EncryptString(string plainText)
    {
        GenerateKeyAndIV(out byte[] key, out byte[] iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// Giải mã chuỗi đã mã hóa AES với Key và IV từ PBKDF2.
    /// </summary>
    public static string DecryptString(string cipherText)
    {
        GenerateKeyAndIV(out byte[] key, out byte[] iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Mã hóa file JSON.
    /// </summary>
    public static void EncryptJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            string encryptedContent = EncryptString(jsonContent);
            File.WriteAllText(filePath, encryptedContent);
            Debug.Log("File JSON đã được mã hóa.");
        }
        else
        {
            Debug.LogError("Không tìm thấy file JSON.");
        }
    }

    /// <summary>
    /// Giải mã file JSON.
    /// </summary>
    public static void DecryptJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string encryptedContent = File.ReadAllText(filePath);
            string decryptedContent = DecryptString(encryptedContent);
            File.WriteAllText(filePath, decryptedContent);
            Debug.Log("File JSON đã được giải mã.");
        }
        else
        {
            Debug.LogError("Không tìm thấy file JSON.");
        }
    }
}