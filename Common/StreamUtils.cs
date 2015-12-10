using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StreamUtils
    {
        /// <summary>
        /// Utility method that fills a StringBuilder
        /// </summary>
        /// <param name="data">Stream containing document data.</param>
        /// <returns>Content of the stream as a string.</returns>
        public static async Task<string> ReadStreamAsync(Stream data, int blockSize=4096)
        {
            var buffer = new char[blockSize];
            var builder = new StringBuilder();

            using (var reader = new StreamReader(data))
            {
                while (!reader.EndOfStream)
                {
                    // StreamReader.ReadAsync() returns number of *chars* read, not number of bytes, in 
                    // spite of what the API docs say. I read the code (MW 2015.11.06)
                    var charsRead = await reader.ReadAsync(buffer, 0, blockSize).ConfigureAwait(false);
                    builder.Append(buffer, 0, charsRead);
                }
            }

            return builder.ToString();
        }


    }
}
