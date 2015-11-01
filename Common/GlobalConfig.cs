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
        public GlobalConfig()
        {
            dynamic config = new Configuration();
            var envFile = config.EnvironmentFile;

            Corpus = config.Bind<CorpusSettings>(new CorpusSettings(envFile));
        }

        public CorpusSettings Corpus { get; }
    }
}
