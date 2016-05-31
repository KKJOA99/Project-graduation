using UnityEngine;
using System.Collections;
using System.Security.Cryptography;


public static class CSecureity {
    private static string _saltForKey;
    private static byte[] _keys;
    private static byte[] _iv;
    private static int keySize = 256;
    private static int blockSize = 128;
    private static int _hashLen = 32;

    static CSecureity() {
        byte[] saltBytes = new byte[] { 25, 36, 77, 51, 43, 14, 75, 93 };
        string randomSeedForKey = "5b6fcb4aaa0a42acae649eba45a506ec";
        string randomSeedForValue = "2e327725789841b5bb5c706db2ad897";
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(randomSeedForKey, saltBytes, 1000);
            _saltForKey = System.Convert.ToBase64String(key.GetBytes(blockSize / 8));
        }
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(randomSeedForValue, saltBytes, 1000);
            _keys = key.GetBytes(keySize / 8);
            _iv = key.GetBytes(blockSize / 8);
        }
    }
    public static string MakeHash( string original ) {
        using(MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider() ) {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(original);
            byte[] hashBytes = m5.ComputeHash(bytes);
            string hashToString = "";
            for( int i = 0 ; i < hashBytes.Length ; ++i )
                hashToString += hashBytes[i].ToString("x2");
            return hashToString;
        }
    }

    public static byte[] Encrypt( byte[] bytesToBeEncrypted ) {
        using( RijndaelManaged aes = new RijndaelManaged() ) {
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.Key = _keys;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            using(ICryptoTransform ct = aes.CreateEncryptor() ) {
                return ct.TransformFinalBlock(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
            }
        }
    }
    public static byte[] Decrypt( byte[] bytesToBeDecrypted ) {
        using(RijndaelManaged aes = new RijndaelManaged() ) {
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.Key = _keys;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            using( ICryptoTransform ct = aes.CreateDecryptor() ) {
                return ct.TransformFinalBlock(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
            }
        }
    }
    public static string Encrypt(string input ) {
        byte[] bytesToBeEncrypted = System.Text.Encoding.UTF8.GetBytes(input);
        byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted);
        return System.Convert.ToBase64String(bytesEncrypted);
    }
    public static string Decrypt(string input ) {
        byte[] bytesToBeDecrypted = System.Convert.FromBase64String(input);
        byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted);
        return System.Text.Encoding.UTF8.GetString(bytesDecrypted);
    }
    private static void SetSecurityValue( string key, string value ) {
        string hideKey = MakeHash(key + _saltForKey);
        string encryptValue = Encrypt(value + MakeHash(value));
        PlayerPrefs.SetString(hideKey, encryptValue);
    }
    public static string GetSecurityValue( string key ) {
        string hideKey = MakeHash(key + _saltForKey);
        string encryptValue = PlayerPrefs.GetString(hideKey);
        if( true == string.IsNullOrEmpty(encryptValue) )
            return string.Empty;
        string valueAndHash = Decrypt(encryptValue);
        if( _hashLen > valueAndHash.Length )
            return string.Empty;
        string savedValue = valueAndHash.Substring(0, valueAndHash.Length - _hashLen);
        string savedHash = valueAndHash.Substring(valueAndHash.Length - _hashLen);
        if( MakeHash(savedValue) != savedHash )
            return string.Empty;
        return savedValue;
    }

    public static void SetInt( string key, int value ) {
        SetSecurityValue(key, value.ToString());
    }

    public static int GetInt( string key, int defaultValue ) {
        string originalValue = GetSecurityValue(key);
        if( true == string.IsNullOrEmpty(originalValue) )
            return defaultValue;
        int result = defaultValue;
        if( false == int.TryParse(originalValue, out result) )
            return defaultValue;
        return result;
    }

}
