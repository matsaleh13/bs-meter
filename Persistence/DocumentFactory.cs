using AnalysisModel;
using System;
using System.Data.HashFunction;
using System.Text;

namespace Persistence
{
    public static class DocumentFactory
    {
        static readonly IHashFunctionAsync _hash = new xxHash();    // non-crypto hash; defaults init=0, size=32 bits

        /// <summary>
        /// Creates an instance of the Document model class, populating it with data provided.
        /// </summary>
        /// <param name="uri">URI identifying the document resource. Ignored if null.</param>
        /// <param name="content">String containing the contents of the document.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded.
        /// If not null, this will override any content type inferred by the framework.</param>
        /// <param name="contentLength">Length of the document contents in bytes.</param>
        /// <returns>An instance of the Document model class populatd with the provided data.</returns>
        public static Document CreateDocument(Uri uri, string content, string contentType, long contentLength)
        {
            // Bah, have to iterate over the entire data again.
            // I don't know any other way to do this right now.
            var hash = _hash.ComputeHash(Encoding.UTF8.GetBytes(content));

            var document = new Document()
            {
                Hash = BitConverter.ToString(hash),
                Source = uri?.AbsoluteUri,
                ContentType = contentType,
                ContentLength = contentLength,
                Content = content,
            };

            return document;
        }


        public static Document CreateDocument(string content, string contentType, long contentLength) => 
            CreateDocument(null, content, contentType, contentLength);


    }
}
