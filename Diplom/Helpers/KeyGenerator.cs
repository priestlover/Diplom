using System.Text;

namespace Diplom.Helpers
{
    public static class KeyGenerator
    {

            const string symbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            public static Random random = new Random();

            public static string GenerateKeyPart()
            {
                string keyPart = string.Empty;

                for (int i = 0; i < 5; i++)
                {
                    keyPart += symbols[random.Next(0, symbols.Length)];
                }

                return keyPart;
            }
            
            public static string GenerateKey() 
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(GenerateKeyPart());
                stringBuilder.Append('-');
                stringBuilder.Append(GenerateKeyPart());
                stringBuilder.Append('-');
                stringBuilder.Append(GenerateKeyPart());
                stringBuilder.Append('-');
                stringBuilder.Append(GenerateKeyPart());
                stringBuilder.Append('-');
                stringBuilder.Append(GenerateKeyPart());
                return stringBuilder.ToString();
            }
    }
}
