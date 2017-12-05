namespace Blocks.Framework.Caching {
    public interface IVolatileToken {
        bool IsCurrent { get; }
    }
}