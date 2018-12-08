namespace Lojinha.Fiap.InfraStructre.Redis
{
    public interface IRedisCache
    {
        string Get(string key);
        void Set(string key, string value);
    }
}