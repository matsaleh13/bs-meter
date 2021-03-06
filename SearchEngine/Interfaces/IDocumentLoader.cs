﻿using System;
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
    interface IDocumentLoader
    {
        /// <summary>
        /// Load a single document identified by a URI into the corpus.
        /// The document may be anywhere in the universe.
        /// </summary>
        /// <param name="resource">Uri that identifies the document to be loaded.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(Uri resource, string contentType = null);


        /// <summary>
        /// Load a single document from the local file system into the corpus.
        /// </summary>
        /// <param name="path">Path to the document in the local file system.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(string path, string contentType = null);


        /// <summary>
        /// Load a single document from a stream into the corpus.
        /// </summary>
        /// <param name="stream">A stream that provides access to the document's contents.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Load(Stream stream, string contentType = null);
    }

    /// <summary>
    /// Responsible for loading raw documents into the corpus using
    /// an asynchronous API.
    /// </summary>
    interface IDocumentLoaderAsync
    {
        /// <summary>
        /// Load a single document identified by a URI into the corpus.
        /// The document may be anywhere in the universe.
        /// </summary>
        /// <param name="resource">Uri that identifies the document to be loaded.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(Uri resource, string contentType = null);


        /// <summary>
        /// Load a single document from the local file system into the corpus.
        /// </summary>
        /// <param name="path">Path to the document in the local file system.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(string path, string contentType = null);


        /// <summary>
        /// Load a single document from a stream into the corpus.
        /// </summary>
        /// <param name="stream">A stream that provides access to the document's contents.</param>
        /// <param name="contentType">MIME type/media type of the contents to be loaded. 
        /// If provided, this will override any content type inferred by the framework.</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> LoadAsync(Stream stream, string contentType = null);
    }
}
