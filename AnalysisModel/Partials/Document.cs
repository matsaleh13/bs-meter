

namespace AnalysisModel
{
    /// <summary>
    /// Partial containing operator implementations for the Document POCO class.
    /// </summary>
    public partial class Document
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Document doc = obj as Document;
            if (doc == null) return false;

            return Equals(doc);
        }


        public bool Equals(Document doc)
        {
            if (doc == null) return false;

            return (Equals(Key, doc.Key)) &&
                   (Equals(Hash, doc.Hash)) &&
                   (Equals(Source, doc.Source)) &&
                   (Equals(ContentType, doc.ContentType)) &&
                   (Equals(ContentLength, doc.ContentLength)) &&
                   (Equals(Content, doc.Content));
        }

        public override int GetHashCode() => Key.GetHashCode() ^
                                             Hash.GetHashCode() ^
                                             Source.GetHashCode() ^
                                             ContentType.GetHashCode() ^
                                             ContentLength.GetHashCode() ^
                                             Content.GetHashCode();

        public override string ToString() => 
            string.Format("{0}: [Key={1}, Hash={2}, ContentType={3}, ContentLength={4}]", 
                base.ToString(), Key, Hash, ContentType, ContentLength);

    }
}
