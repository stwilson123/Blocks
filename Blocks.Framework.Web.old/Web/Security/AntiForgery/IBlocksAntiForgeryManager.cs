namespace Blocks.Web.Security.AntiForgery
{
    public interface IBlocksAntiForgeryManager
    {
        IBlocksAntiForgeryConfiguration Configuration { get; }

        string GenerateToken();
    }
}
