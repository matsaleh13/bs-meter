using AnalysisModel;
using DataAccess.Interfaces;
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
        readonly IHashFunctionAsync _hash = new xxHash();    // non-crypto hash; defaults init=0, size=32 bits

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
                    // StreamReader.ReadAsync() returns number of *chars* read, not number of bytes, in 
                    // spite of what the API docs say. I read the code (MW 2015.11.06)
                    var charsRead = await reader.ReadAsync(buffer, 0, _blockSize).ConfigureAwait(false);
                    builder.Append(buffer, 0, charsRead);
                }
            }

            return builder.ToString();
        }


        public async Task<bool> LoadAsync(string path, string contentType = null)
        {
            var fullPath = Path.GetFullPath(path);

            return await LoadAsync(new Uri(string.Format("file:///{0}", fullPath)), contentType);
        }

        public async Task<bool> LoadAsync(Uri resource, string contentType = null)
        {
            using (var response = await WebRequest.Create(resource).GetResponseAsync().ConfigureAwait(false))
            {
                using (var data = response.GetResponseStream())
                {
                    var content = await ReadStreamAsync(data).ConfigureAwait(false);

                    // Bah, have to iterate over the entire data again.
                    // I don't know any other way to do this right now.
                    // TODO: DRY, and maybe a better place for this.
                    var hash = _hash.ComputeHash(Encoding.UTF8.GetBytes(content));

                    var document = new Document()
                    {
                        Hash = BitConverter.ToString(hash),
                        Source = resource,
                        ContentType = contentType ?? response.ContentType,
                        ContentLength = response.ContentLength,
                        Content = content,
                    };

                    return await _repository.AddAsync(document).ConfigureAwait(false);
                }
            }
        }

        public async Task<bool> LoadAsync(Stream data, string contentType=null)
        {
            var content = await ReadStreamAsync(data).ConfigureAwait(false);

            // Bah, have to iterate over the entire data again.
            // I don't know any other way to do this right now.
            // TODO: DRY, and maybe a better place for this.
            var hash = _hash.ComputeHash(Encoding.UTF8.GetBytes(content));

            var document = new Document()
            {
                Hash = BitConverter.ToString(hash),
                ContentType = contentType ?? "",
                ContentLength = content.Length,
                Content = content,
            };

            return await _repository.AddAsync(document).ConfigureAwait(false);
        }

    }
}