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

            Corpus = config.Bind<CorpusSettings>();
        }

        public CorpusSettings Corpus { get; }
    }
}
