using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterAnalyzerTests
    {
        string Data = @"> 90# on your telephone 
> 
> I dialed '0', to check this out, asked the operator, who confirmed that
> this was correct so please pass it on . . . (l also checked out
> Snopes.com <http://snopes.com/> . This is true, and also applies to cell
> phones!)
> 
> PASS ON TO EVERYONE YOU KNOW 
> 
> I received a telephone call last evening from an individual identifying
> himself as an AT&T Service Technician (could also be Telus) who was
> conducting a test on the telephone lines. He stated that to complete the
> test I should touch nine(9), zero(0), the pound sign (#), and then hang
> up. Luckily, I was suspicious and refused. 
> Up on contacting the telephone company, I was informed that by pushing
> 90#, you give the requesting individual full acces s to your telephone
> line, which enables them to place long distance calls billed to your
> home phone number. 
> 
> I was further informed that this scam has been originating from many
> local jails/prisons DO NOT press 90# for ANYONE. 
> The GTE Security Department requested that I share this information with
> EVERYONE I KNOW. 
> 
> After checking with Verizon they also said it was true, so do not dial
> 90# for anyone !!!!! PLEASE HIT THAT FORWARD BUTTON AND PASS THIS ON TO
> EVERYONE YOU KNOW!!!
";
        int CharacterCount;
        int PunctuationCount;
        int WhitespaceCount;
        int UpperCount;
        int OtherCount;    

        Stream DataStream;


        [SetUp]
        public void SetUp()
        {
            CharacterCount = Data.Length;
            PunctuationCount = Data.Count(x => char.IsPunctuation(x));
            WhitespaceCount = Data.Count(x => char.IsWhiteSpace(x));
            UpperCount = Data.Count(x => char.IsUpper(x));
            OtherCount = CharacterCount - PunctuationCount - WhitespaceCount - UpperCount;

            var dataBytes = Encoding.UTF8.GetBytes(Data);
            DataStream = new MemoryStream(dataBytes);
        }

        [TestCase(1)]
        [TestCase(32)]
        [TestCase(128)]
        [TestCase(512)]
        [TestCase(1024)]
        public void AnalyzeTest(int blockSize)
        {
            var analyzer = new CharacterAnalyzer();
            var result = (CharacterAnalyzerResult)analyzer.Analyze(DataStream, blockSize);

            Assert.AreEqual(CharacterCount, result.CharacterCount.Count);
            Assert.AreEqual(PunctuationCount, result.Punctuation.Counter.Count);
            Assert.AreEqual(WhitespaceCount, result.Whitespace.Counter.Count);
            Assert.AreEqual(UpperCount, result.UpperCase.Counter.Count);
            Assert.AreEqual(OtherCount, result.Other.Counter.Count);
        }

        [TestCase(1)]
        [TestCase(32)]
        [TestCase(128)]
        [TestCase(512)]
        [TestCase(1024)]
        public void AnalyzeAsyncTest(int blockSize)
        {
            var analyzer = new CharacterAnalyzer();
            var result = (CharacterAnalyzerResult)Task.Run(async () => await analyzer.AnalyzeAsync(DataStream, blockSize).ConfigureAwait(false)).Result;

            Assert.AreEqual(CharacterCount, result.CharacterCount.Count);
            Assert.AreEqual(PunctuationCount, result.Punctuation.Counter.Count);
            Assert.AreEqual(WhitespaceCount, result.Whitespace.Counter.Count);
            Assert.AreEqual(UpperCount, result.UpperCase.Counter.Count);
            Assert.AreEqual(OtherCount, result.Other.Counter.Count);
        }
    }
}