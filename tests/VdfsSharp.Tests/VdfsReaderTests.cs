using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;

namespace VdfsSharp.Tests
{
    [TestClass]
    [DeploymentItem("Samples/test.vdf")]
    public class VdfsReaderTests
    { 
        VdfsReader vdfsReader;

        [TestInitialize]
        public void Init()
        {
            vdfsReader = new VdfsReader("Samples/test.vdf");
        }

        [TestMethod]
        public void ReadHeader_Test()
        {
            Assert.AreEqual("test_comment", vdfsReader.Header.Comment);
            Assert.AreEqual("PSVDSC_V2.00\n\r\n\r", vdfsReader.Header.Signature);
            Assert.AreEqual((uint)6, vdfsReader.Header.EntryCount);
            Assert.AreEqual((uint)2, vdfsReader.Header.FileCount);
            Assert.AreEqual(new DateTime(2018, 06, 20, 11, 04, 05), vdfsReader.Header.TimeStamp);
            Assert.AreEqual((uint)0, vdfsReader.Header.DataSize);
            Assert.AreEqual((uint)0x128, vdfsReader.Header.RootOffset);
            Assert.AreEqual(Vdfs.EntrySize, vdfsReader.Header.EntrySize);
        }
     
        [TestMethod]
        public void ReadEntries_Test()
        {
            var entries = vdfsReader.ReadEntries(true).ToList();

            CollectionAssert.AreEqual(expectedEntries, entries);
        }
    
        private static VdfsEntry[] expectedEntries = new VdfsEntry[]
        {
            new VdfsEntry
            {
                Name = "_WORK",
                Offset = 1,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry
            {
                Name = "DATA",
                Offset = 2,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry
            {
                Name = "SCRIPTS",
                Offset = 4,
                Size = 0,
                Type = Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry
            {
                Name = "TEXTURES",
                Offset = 5,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry
            {
                Name = "SCRIPT.D",
                Offset = 0x308,
                Size = 0 ,
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new Byte[0],
            },
            new VdfsEntry
            {
                Name = "TEXTURE.TEX",
                Offset = 0x308,
                Size = 0,
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0],
            },
        };
    }
}
