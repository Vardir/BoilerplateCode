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

            if (lastSlashIndex <= 0) return path;

            return path.Substring(lastSlashIndex + 1);
        }
    }
}