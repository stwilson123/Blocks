namespace Blocks.ResourcesModule.ResourceManifestProviders
{
    public class PathBuilder
    {
        public static string BuilderScripts(string sourcePath)
        {
            return "Scripts/" + (sourcePath?? string.Empty );
        }
        
        public static string BuilderStyle(string sourcePath)
        {
            return "Content/" + (sourcePath?? string.Empty );
        }
    }
}