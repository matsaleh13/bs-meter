using AnalysisModel;
using Common;
using DataAccess.Interfaces;
using Persistence;
using SearchEngine.Interfaces;
using System;
using System.Data.HashFunction;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    /// <summary>
    /// Responsible for loading raw documents into the corpus.
    /// </summary>
    public class DocumentLoaderAsync : IDocumentLoaderAsync
    {
        readonly int _blockSize;
        readonly IRepositoryAsync<Document> _repository;

        public DocumentLoaderAsync(IRepositoryAsync<Document> repository, int blockSize=4096)
        {
            _repository = repository;
            _blockSize = blockSize;
        }


        /// <summary>
        /// Load a single document from the local file system into the corpus.
        /// </summary>
        /// <param name="path">Path to the document in the local file system.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded.
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> LoadAsync(string path, string contentType = null)
        {
            var fullPath = Path.GetFullPath(path);

            return await LoadAsync(new Uri(string.Format("file:///{0}", fullPath)), contentType);
        }

        /// <summary>
        /// Load a single document identified by a URI into the corpus.
        /// The document may be anywhere in the universe.
        /// </summary>
        /// <param name="resource">Uri that identifies the document to be loaded.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded.
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> LoadAsync(Uri resource, string contentType = null)
        {
            using (var response = await WebRequest.Create(resource).GetResponseAsync().ConfigureAwait(false))
            {
                using (var data = response.GetResponseStream())
                {
                    var content = await StreamUtils.ReadStreamAsync(data).ConfigureAwait(false);

                    Document document = DocumentFactory.CreateDocument(resource, content, contentType ?? response.ContentType, response.ContentLength);

                    return await _repository.AddAsync(document).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Load a single document from a stream into the corpus.
        /// </summary>
        /// <param name="stream">A stream that provides access to the document's contents.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded.
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> LoadAsync(Stream data, string contentType=null)
        {
            var content = await StreamUtils.ReadStreamAsync(data).ConfigureAwait(false);

            Document document = DocumentFactory.CreateDocument(content, contentType, content.Length);

            return await _repository.AddAsync(document).ConfigureAwait(false);
        }

    }
}