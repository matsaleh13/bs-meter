using DataAccess;
using DataAccess.Interfaces;
using System;
using System.Runtime.Serialization;


namespace AnalysisModel
{
    /// <summary>
    /// Represents a single document in the corpus.
    /// </summary>
    [DataContract]
    public partial class Document : IEntity
    {
        /// <summary>
        /// The unique key for this document.
        /// </summary>
        [DataMember]
        public Key Key { get; set; }

        /// <summary>
        /// A cryptographic hash of the document's contents.
        /// For use in checking for duplicates.
        /// </summary>
        [DataMember]
        public string Hash { get; set; }

        /// <summary>
        /// The URI of the original source of the document, if available.
        /// Must be a valid URI, null, or empty string.
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        /// <summary>
        /// The document's media type as would be represented in the
        /// HTTP Content-type header.
        /// May be null or empty string if not set or unknown.
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// The document's length as would be represented in the 
        /// HTTP Content-length header.
        /// </summary>
        [DataMember]
        public long ContentLength { get; set; }

        /// <summary>
        /// The data of the document contents in raw form.
        /// </summary>
        [DataMember]
        public string Content { get; set; }

    }
}
