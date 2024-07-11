namespace LocalServices
{
    public class RandomGenerator
    {
        public string CreateRandomString(){
        Random random = new Random();
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(caracteres, 20)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}