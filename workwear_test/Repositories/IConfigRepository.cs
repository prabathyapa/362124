namespace workwear_test.Repositories
{
    public interface IConfigRepository
    {
        void Add(string key, string value);
        string Get(string key);
        void Update(string key, string value);
    }
}