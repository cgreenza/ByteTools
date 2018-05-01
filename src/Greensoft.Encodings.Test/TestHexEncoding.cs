using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Greensoft.Encodings.Test
{

    [TestClass]
    public class TestHexEncoding
    {
        [TestMethod]
        public void GetString_AllByteValues()
        {
            var buf = new byte[1];
            for (byte testByte = 0; testByte < 0xff; testByte++)
            {
                buf[0] = testByte;
                var actual = HexEncoding.GetString(buf);
                Assert.AreEqual(testByte.ToString("X2"), actual);
            }
        }

        [TestMethod]
        public void GetString_EmptyByteArray()
        {
            var actual = HexEncoding.GetString(new byte[0]);
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void GetString_RandomBytes()
        {
            var buf = new byte[1000];
            (new Random()).NextBytes(buf);

            var expected = BitConverter.ToString(buf).Replace("-", "");
            var actual = HexEncoding.GetString(buf);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetBytes_EmptyString()
        {
            var actual = HexEncoding.GetBytes("");
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void GetBytes_ThrowsExceptionForOddLength()
        {
            Assert.ThrowsException<FormatException>(() => HexEncoding.GetBytes("A"));
            Assert.ThrowsException<FormatException>(() => HexEncoding.GetBytes("AAA"));
        }

        [TestMethod]
        public void GetBytes_AllUpperCaseHexChars()
        {
            for (byte testByte = 0; testByte < 0xff; testByte++)
            {
                var actual = HexEncoding.GetBytes(testByte.ToString("X2"));
                Assert.AreEqual(1, actual.Length);
                Assert.AreEqual(testByte, actual[0]);
            }
        }

        [TestMethod]
        public void GetBytes_AllLowerCaseHexChars()
        {
            for (byte testByte = 0; testByte < 0xff; testByte++)
            {
                var actual = HexEncoding.GetBytes(testByte.ToString("x2"));
                Assert.AreEqual(1, actual.Length);
                Assert.AreEqual(testByte, actual[0]);
            }
        }

        [TestMethod]
        public void GetBytes_RandomBytes()
        {
            var expected = new byte[1000];
            (new Random()).NextBytes(expected);

            var hexString = BitConverter.ToString(expected).Replace("-", "");
            var actual = HexEncoding.GetBytes(hexString);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetBytes_ThrowsExceptionForAllInvalidCharacters()
        {
            for (ushort testChar = 0; testChar < ushort.MaxValue; testChar++)
            {
                var c = (char)testChar;
                if (char.IsDigit(c) ||
                    c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' ||
                    c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f')
                    continue; // skip valid chars

                Assert.ThrowsException<FormatException>(() => HexEncoding.GetBytes("0" + new string(new char[] { c })));
            }
        }
    }
}
