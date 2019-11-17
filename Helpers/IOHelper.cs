namespace Vardirsoft.Shared.Helpers
{
    public static class IOHelper
    {
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');
            var lastSlashIndex = normalizedPath.LastIndexOf('\\');

            return lastSlashIndex <= 0 ? path : path.Substring(lastSlashIndex + 1);
        }
    }
}