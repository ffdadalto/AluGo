using System.Security.Cryptography;

namespace AluGo.Services
{
    public static class MD5Hash
    {
        public static string Criptografar(string Senha)
        {
            try
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(Senha);
                byte[] hash = MD5.HashData(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }
    }
}
