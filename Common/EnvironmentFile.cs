using System;
using System.IO;
using Common.Logging;


namespace Common
{
    public static class EnvironmentFile
    {
        static ILog Log = LogManager.GetLogger(typeof(EnvironmentFile));

        const string COMMENT = "#";

        public static bool Load(string path)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Failed to load environment file at [{}].", ex, path);

                return false;
            }

            foreach (var rawline in lines)
            {
                var line = rawline;

                // Strip comments
                var commentPos = line.IndexOf(COMMENT, StringComparison.Ordinal);
                if (commentPos >= 0) line = line.Remove(commentPos);

                // Nuke empty lines
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;


                string[] parsed;
                string name;
                string value;

                parsed = line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (parsed.Length != 2)
                {
                    Log.ErrorFormat("Failed to parse line [{}] in environment file [{}]", line, path);
                    return false;
                }

                name = parsed[0].Trim();
                value = parsed[1].Trim();

                Log.InfoFormat("Setting env var [{}] to [{}].", name, value);
                Environment.SetEnvironmentVariable(name, value);
            }

            return true;
        }
    }
}
