using System;
using System.IO;

namespace AnalysisLib.Interfaces
{
    /// <summary>
    /// Represents a single body of data to be analyzed and scored.
    /// This will usually contain text, HTML or XML, with embedded URIs 
    /// and/or MIME attachments.
    /// 
    /// For convenience and ease of preprocessing we may classify the documents by 
    /// type, such as email, web (remote), PDF, and the like. Probably best to
    /// use known MIME types for this.
    /// </summary>
    interface IDocument
    {
        /// <summary>
        /// The Uniform Resource Identifier for this document.
        /// This should describe the document uniquely across all documents in the domain.
        /// </summary>
        Uri URI { get; }

        /// <summary>
        /// The raw contents of the document. 
        /// If the document is remote, this may not be populated until it is retrieved.
        /// </summary>
        Stream Data { get; }
    }
}
