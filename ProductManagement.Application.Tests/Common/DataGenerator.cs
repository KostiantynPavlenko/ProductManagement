using System.Text;

namespace ProductManagement.Application.Tests.Common;

public static class DataGenerator
{
     private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

     public static string GenerateString(int length)
     {
          var random = new Random();

          var randomChars = Enumerable.Range(0, length)
               .Select(_ => Alphabet[random.Next(0, Alphabet.Length)]);
        
          return new string(randomChars.ToArray());
     }
}