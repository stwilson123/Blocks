namespace Blocks.Owin
{
    public class BlocksOwinOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseEmbeddedFiles { get; set; }

        public BlocksOwinOptions()
        {
            UseEmbeddedFiles = true;
        }
    }
}