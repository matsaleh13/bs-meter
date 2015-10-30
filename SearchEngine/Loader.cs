using SearchEngine.Interfaces;
using System;
using System.IO;

namespace SearchEngine
{
    /// <summary>
    /// Responsible for loading raw documents into the corpus.
    /// </summary>
    public class Loader : ILoader
    {
        public bool Load(Uri resource)
        {
            throw new NotImplementedException();
        }

        public bool Load(string path)
        {
            throw new NotImplementedException();
        }

        public bool Load(Stream stream)
        {
            throw new NotImplementedException();
        }

    }
}