using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;

namespace UnitTests.OrganisationServiceTests
{
    public class ParseQuranReference : OrganisationServiceTestContext
    {
        [Test]
        public void ParsesTextCorrectlyWithQuranReference()
        {
            MethodToTest(()=>service.ParseForQuranReferences(A<string>.Ignored));

            var testTextWithRef = "some Rule Q2:34 with a ref";

            var result = service.ParseForQuranReferences(testTextWithRef).ToList();

            Assert.AreEqual(3,result.Count);
            Assert.AreEqual("some Rule ",result[0].Text);
            Assert.IsTrue(result[0].IsPlainText);
            var qr = result[1].QuranReference;
            Assert.AreEqual(2,qr.Surah);
            Assert.AreEqual(34,qr.Verse);
            Assert.AreEqual(" with a ref", result[2].Text);
            Assert.IsTrue(result[2].IsPlainText);
        }
        [Test]
        public void ParsesTextCorrectlyWithMultipleQuranReferences()
        {
            MethodToTest(() => service.ParseForQuranReferences(A<string>.Ignored));

            var testTextWithRef = "some Rule Q2:34 with a ref and another ref Q2:45";

            var result = service.ParseForQuranReferences(testTextWithRef).ToList();

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("some Rule ", result[0].Text);
            Assert.IsTrue(result[0].IsPlainText);
            var qr = result[1].QuranReference;
            Assert.AreEqual(2, qr.Surah);
            Assert.AreEqual(34, qr.Verse);
            Assert.AreEqual(" with a ref and another ref ", result[2].Text);
            Assert.IsTrue(result[2].IsPlainText);
            var qr2 = result[3].QuranReference;
            Assert.AreEqual(2, qr2.Surah);
            Assert.AreEqual(45, qr2.Verse);


        }
    }
}
