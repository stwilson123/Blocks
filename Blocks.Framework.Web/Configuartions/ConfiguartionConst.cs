namespace Blocks.Framework.Web.Configuartions
{
    public static class ConfiguartionConst
    {
        public static readonly string DatabaseType = "DatabaseType";
    }

    public enum DatabaseType
    {
        Sqlserver,
        Oralce,
        MySql
    }
}