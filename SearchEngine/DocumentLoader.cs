using AnalysisModel;
using DataAccess.Interfaces;
using SearchEngine.Interfaces;
using System;
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
        /// Utility method that fills a StringBuilder
        /// </summary>
        /// <param name="data">Stream containing document data.</param>
        /// <returns>Content of the stream as a string.</returns>
        private async Task<string> ReadStreamAsync(Stream data)
        {
            var buffer = new char[_blockSize];
            var builder = new StringBuilder();

            using (var reader = new StreamReader(data))
            {
                while (!reader.EndOfStream)
                {
                    await reader.ReadAsync(buffer, 0, _blockSize).ConfigureAwait(false);
                    builder.Append(buffer);
                }
            }

            return builder.ToString();
        }

        public async Task<bool> LoadAsync(string path, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoadAsync(Uri resource)
        {
            using (var response = await WebRequest.Create(resource).GetResponseAsync().ConfigureAwait(false))
            {
                using (var stream = response.GetResponseStream())
                {
                    var content = await ReadStreamAsync(stream).ConfigureAwait(false);

                    var document = new Document()
                    {
                        ContentType = response.ContentType,
                        ContentLength = response.ContentLength,
                        Content = content,
                    };

                    return await _repository.AddAsync(document).ConfigureAwait(false);
                }
            }
        }

        public async Task<bool> LoadAsync(Stream data, string contentType)
        {
            var content = await ReadStreamAsync(data).ConfigureAwait(false);

            var document = new Document()
            {
                ContentType = contentType,
                ContentLength = content.Length,
                Content = content,
            };

            return await _repository.AddAsync(document).ConfigureAwait(false);
        }

    }
}