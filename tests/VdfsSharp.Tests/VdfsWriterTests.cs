using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace VdfsSharp.Tests
{
    [TestClass]
    [DeploymentItem("Samples/VdfsWriter")]
    public class VdfsWriterTests
    {
        [TestMethod]
        public void VdfsWriter_Test()
        {
            var writer = new VdfsWriter(@"test.vdf", "test", GothicVersion.Gothic2);

            writer.AddDirectory(@"Samples\VdfsWriter\1");

            CollectionAssert.AreEqual(expectedEntries, writer.Entries);  
        }

        [TestMethod]
        public void VdfsWriter_Test2()
        {
            var writer = new VdfsWriter(@"test2.vdf", "test", GothicVersion.Gothic2);

            writer.AddDirectory(@"Samples\VdfsWriter\2");

            CollectionAssert.AreEqual(expectedEntries2, writer.Entries);
        }

        [TestMethod]
        public void VdfsWriter_Test3()
        {
            var writer = new VdfsWriter(@"test3.vdf", "test", GothicVersion.Gothic2);

            writer.AddDirectory(@"Samples\VdfsWriter\3");

            CollectionAssert.AreEqual(expectedEntries3, writer.Entries);
        }

        [TestMethod]
        public void VdfsWriter_Test4()
        {
            var writer = new VdfsWriter(@"test4.vdf", "test", GothicVersion.Gothic2);

            writer.AddDirectory(@"Samples\VdfsWriter\4");

            CollectionAssert.AreEqual(expectedEntries4, writer.Entries);
        }

        [TestMethod]
        public void VdfsWriter_Test5()
        {
            var writer = new VdfsWriter(@"test5.vdf", "test", GothicVersion.Gothic2);

            writer.AddDirectory(@"Samples\VdfsWriter\5");

            CollectionAssert.AreEqual(expectedEntries5, writer.Entries);
        }

        private static List<VdfsEntry> expectedEntries = new List<VdfsEntry>()
        {
            new VdfsEntry()
            {
                Name = "_WORK",
                Offset = 1,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry()
            {
                Name = "DATA",
                Offset = 2,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry()
            {
                Name = "SCRIPTS",
                Offset = 4,
                Size = 0,
                Type = Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry()
            {
                Name = "TEXTURES",
                Offset = 5,
                Size = 0,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null,
            },
            new VdfsEntry()
            {
                Name = "SCRIPT.D",
                Size = 0 ,
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0],
            },
            new VdfsEntry()
            {
                Name = "TEXTURE.TEX",
                Size = 0,
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0],
            },
        };

        private static List<VdfsEntry> expectedEntries2 = new List<VdfsEntry>()
        {
            new VdfsEntry()
            {
                Name = "DIR",
                Offset = 2,
                Size = 0,
                Type = Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null
            },
            new VdfsEntry()
            {
                Name = "FILE1.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "FILE2.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            }
        };

        private static List<VdfsEntry> expectedEntries3 = new List<VdfsEntry>()
        {
            new VdfsEntry()
            {
                Name = "_WORK",
                Offset = 2,
                Size = 0,
                Type = Vdfs.EntryType.Directory,
                Attributes = 0,
                Content = null
            },
            new VdfsEntry()
            {
                Name = "_WORK2",
                Offset = 3,
                Size = 0,
                Type = Vdfs.EntryType.Directory | Vdfs.EntryType.Last,
                Attributes = 0,
                Content = null
            },
            new VdfsEntry()
            {
                Name = "TEST.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "TEST2.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            }
        };

        private static List<VdfsEntry> expectedEntries4 = new List<VdfsEntry>()
        {
            new VdfsEntry()
            {
                Name = "DIR1",
                Type = Vdfs.EntryType.Directory,
                Offset = 2,
                Attributes = 0,
                Content = null
            },
            new VdfsEntry()
            {
                Name = "TEST1.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "DIR2",
                Type = Vdfs.EntryType.Directory,
                Offset = 4,
                Attributes = 0,
                Content = null
            },
            new VdfsEntry()
            {
                Name = "TEST2.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "TEST3.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
        };

        private static List<VdfsEntry> expectedEntries5 = new List<VdfsEntry>()
        {
            new VdfsEntry()
            {
                Name = "TEST1.TXT",
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "TEST2.TXT",
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            },
            new VdfsEntry()
            {
                Name = "TEST3.TXT",
                Type = Vdfs.EntryType.Last,
                Attributes = Vdfs.FileAttribute.Archive,
                Content = new byte[0]
            }
        };
    }
}
