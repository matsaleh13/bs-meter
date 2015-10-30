﻿using DataAccess;
using DataAccess.Interfaces;
using System;
using System.Net.Mime;

namespace AnalysisModel
{
    /// <summary>
    /// Represents a single document in the corpus.
    /// </summary>
    public class Document : IEntity
    {
        /// <summary>
        /// The unique key for this document.
        /// </summary>
        public Key Key { get; set; }

        /// <summary>
        /// The URI of the original source of the document, if available.
        /// </summary>
        public Uri Source { get; set; }

        /// <summary>
        /// The document's media type as would be represented in the
        /// HTTP Content-type header.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The document's length as would be represented in the 
        /// HTTP Content-length header.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// The data of the document contents in raw form.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
