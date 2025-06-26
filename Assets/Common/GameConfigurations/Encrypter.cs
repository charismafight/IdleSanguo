using System.IO;
using System.Security.Cryptography;

namespace Assets.Common.GameConfigurations
{
    public static class Encrypter
    {
        private const string password = "pwd";
        private const ulong FC_TAG = 0xFC010203040506CF;
        private const int BUFFER_SIZE = 128 * 1024;
        private static RandomNumberGenerator rand = new RNGCryptoServiceProvider();
        private static bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        private static SymmetricAlgorithm CreateRijndael(byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);
            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }
        private static byte[] GenerateRandomBytes(int count)
        {
            byte[] bytes = new byte[count];
            rand.GetBytes(bytes);
            return bytes;
        }

        public static void EncryptFile(string inFile, string outFile)
        {
            using (FileStream fin = new(inFile, FileMode.Open, FileAccess.Read), fout = File.OpenWrite(outFile))
            {
                long lSize = fin.Length;
                int size = (int)lSize;
                byte[] bytes = new byte[BUFFER_SIZE];
                int read = -1;
                int value = 0;

                byte[] IV = GenerateRandomBytes(16);
                byte[] salt = GenerateRandomBytes(16);

                SymmetricAlgorithm sma = CreateRijndael(salt);
                sma.IV = IV;

                fout.Write(IV, 0, IV.Length);
                fout.Write(salt, 0, salt.Length);

                HashAlgorithm hasher = SHA256.Create();
                using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write), chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    BinaryWriter bw = new BinaryWriter(cout);
                    bw.Write(lSize);
                    bw.Write(FC_TAG);


                    while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        cout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                    }
                    chash.Flush();
                    chash.Close();

                    byte[] hash = hasher.Hash;

                    cout.Write(hash, 0, hash.Length);
                    cout.Flush();
                    cout.Close();
                }
            }
        }

        public static void DecryptFile(string inFile, string outFile)
        {
            using (FileStream fin = File.OpenRead(inFile), fout = File.OpenWrite(outFile))
            {
                int size = (int)fin.Length;
                byte[] bytes = new byte[BUFFER_SIZE];
                int read = -1;
                int value = 0;
                int outvalue = 0;

                byte[] IV = new byte[16];
                fin.Read(IV, 0, 16);
                byte[] salt = new byte[16];
                fin.Read(salt, 0, 16);

                SymmetricAlgorithm sma = CreateRijndael(salt);
                sma.IV = IV;
                value = 32;
                long lSize = -1;

                HashAlgorithm hasher = SHA256.Create();

                using (CryptoStream cin = new CryptoStream(fin, sma.CreateDecryptor(), CryptoStreamMode.Read), chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    BinaryReader br = new BinaryReader(cin);
                    lSize = br.ReadInt64();
                    ulong tag = br.ReadUInt64();


                    if (FC_TAG != tag)
                    {
                        throw new CryptographicException("文件已被破坏！");
                    }

                    long numReads = lSize / BUFFER_SIZE;
                    long slack = (long)lSize % BUFFER_SIZE;


                    for (int i = 0; i < numReads; ++i)
                    {
                        read = cin.Read(bytes, 0, bytes.Length);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outvalue += read;
                    }

                    if (slack > 0)
                    {
                        read = cin.Read(bytes, 0, (int)slack);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outvalue += read;
                    }
                    chash.Flush();
                    chash.Close();

                    byte[] curHash = hasher.Hash;
                    byte[] oldHash = new byte[hasher.HashSize / 8];
                    read = cin.Read(oldHash, 0, oldHash.Length);
                    if ((oldHash.Length != read) || (!CheckByteArrays(oldHash, curHash)))
                        throw new CryptographicException("文件已被破坏！");

                    cin.Flush();
                    cin.Close();
                }

                fout.Flush();
                fout.Close();

                if (outvalue != lSize)
                    throw new CryptographicException("文件大小不匹配！");
            }
        }
    }
}
