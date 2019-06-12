namespace Blocks.Framework.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}