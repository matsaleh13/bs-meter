using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Interfaces
{
    /// <summary>
    /// Responsible for loading raw documents into the corpus.
    /// </summary>
    interface ILoader
    {
        /// <summary>
        /// Load a single document from the local file system into the corpus.
        /// </summary>
        /// <param name="path">Path to the document in the local file system.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(string path);


        /// <summary>
        /// Load a single document identified by a URI into the corpus.
        /// The document may be anywhere in the universe.
        /// </summary>
        /// <param name="resource">Uri that identifies the document to be loaded.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(Uri resource);


        /// <summary>
        /// Load a single document from a stream into the corpus.
        /// </summary>
        /// <param name="stream">A stream that provides access to the document's contents.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(Stream stream);
    }

    /// <summary>
    /// Responsible for loading raw documents into the corpus using
    /// an asynchronous API.
    /// </summary>
    interface ILoaderAsync
    {
        /// <summary>
        /// Load a single document from the local file system into the corpus.
        /// </summary>
        /// <param name="path">Path to the document in the local file system.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(string path);


        /// <summary>
        /// Load a single document identified by a URI into the corpus.
        /// The document may be anywhere in the universe.
        /// </summary>
        /// <param name="resource">Uri that identifies the document to be loaded.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(Uri resource);


        /// <summary>
        /// Load a single document from a stream into the corpus.
        /// </summary>
        /// <param name="stream">A stream that provides access to the document's contents.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(Stream stream);
    }
}
