using System;
using System.IO;
using Common.Logging;


namespace Common
{
    public static class EnvironmentFile
    {
        static ILog Log = LogManager.GetLogger(typeof(EnvironmentFile));

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

            foreach (var line in lines)
            {
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
