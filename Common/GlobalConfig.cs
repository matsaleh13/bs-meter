using Formo;


namespace Common
{
    public class GlobalConfig
    {
        public static GlobalConfig Instance { get; }

        static GlobalConfig()
        {
            Instance = new GlobalConfig();
        }

        /// <summary>
        /// Require use of singleton except for testing.
        /// </summary>
        internal GlobalConfig()
        {
            dynamic config = new Configuration();
            var envFile = config.EnvironmentFile;

            Corpus = config.Bind<ConnectionSettings>(new ConnectionSettings(envFile));
        }

        public ConnectionSettings Corpus { get; }
    }
}
