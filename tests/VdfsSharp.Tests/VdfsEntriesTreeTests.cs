using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace VdfsSharp.Tests
{
    [TestClass]
    public class VdfsEntriesTreeGeneratorTests
    {
        string generateVdfsEntriesTreeView(VdfsEntry[] entries)
        {
            var treeGenerator = new VdfsEntriesTreeGenerator(entries);

            var entriesTree = treeGenerator.Generate();

            return entriesTree.GetTreeView();
        }

        string generateVdfsEntriesTreeView(string vdfFile)
        {
            var vdf = new VdfsReader(vdfFile);

            var entries = vdf.ReadEntries(false).ToArray();

            return generateVdfsEntriesTreeView(entries);
        }

        [TestMethod]
        public void Generate_Test1()
        {
            var entries = new VdfsEntryListBuilder()
                /*0*/.AddLastDirectory("_WORK", 1)
                /*1*/.AddLastDirectory("DATA", 2)
                /*2*/.AddDirectory("SCRIPTS", 4)
                /*3*/.AddLastDirectory("TEXTURES", 5)
                /*4*/.AddLast("SCRIPT.D")
                /*5*/.AddLast("TEXTURE.TEX").Entries.ToArray();

            var expected = new StringBuilder();
            expected.AppendLine("+- _WORK");
            expected.AppendLine("   +- DATA");
            expected.AppendLine("      +- SCRIPTS");
            expected.AppendLine("      |  +- SCRIPT.D");
            expected.AppendLine("      +- TEXTURES");
            expected.AppendLine("         +- TEXTURE.TEX");

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), expected.ToString());
            Assert.AreEqual(generateVdfsEntriesTreeView("Samples/test.vdf"), expected.ToString());
        }

        [TestMethod]
        public void Generate_Test2()
        {
            var entries = new VdfsEntryListBuilder()
                /*0*/.AddDirectory("1", 2)
                /*1*/.AddLastDirectory("2", 6)
                /*2*/.AddDirectory("_WORK1_1", 4)
                /*3*/.AddLastDirectory("_WORK1_2", 5)
                /*4*/.AddLast("TEST1_1.TXT")
                /*5*/.AddLast("TEST1_2.TXT")
                /*6*/.AddDirectory("_WORK2_1", 8)
                /*7*/.AddLastDirectory("_WORK2_2", 9)
                /*8*/.AddLast("TEST2_1.TXT")
                /*9*/.AddLast("TEST2_2.TXT").Entries.ToArray();

            var expected = new StringBuilder();
            expected.AppendLine("+- 1");
            expected.AppendLine("|  +- _WORK1_1");
            expected.AppendLine("|  |  +- TEST1_1.TXT");
            expected.AppendLine("|  +- _WORK1_2");
            expected.AppendLine("|     +- TEST1_2.TXT");
            expected.AppendLine("+- 2");
            expected.AppendLine("   +- _WORK2_1");
            expected.AppendLine("   |  +- TEST2_1.TXT");
            expected.AppendLine("   +- _WORK2_2");
            expected.AppendLine("      +- TEST2_2.TXT");

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), expected.ToString());
            Assert.AreEqual(generateVdfsEntriesTreeView("Samples/test2.vdf"), expected.ToString());
        }

        [TestMethod]
        public void Generate_Test3()
        {
            var entries = new VdfsEntryListBuilder()
                /*0*/.AddDirectory("DIR", 2)
                /*1*/.AddLast("FILE1.TXT")
                /*2*/.AddLast("FILE2.TXT").Entries.ToArray();

            var expected = new StringBuilder();
            expected.AppendLine("+- DIR");
            expected.AppendLine("|  +- FILE2.TXT");
            expected.AppendLine("+- FILE1.TXT");

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), expected.ToString());
            Assert.AreEqual(generateVdfsEntriesTreeView("Samples/test3.vdf"), expected.ToString());
        }

        [TestMethod]
        public void Generate_Test4()
        {
            var entries = new VdfsEntryListBuilder()
                /*0*/.Add("HEIßE.TXT")
                /*1*/.AddLast("Ó.TXT").Entries.ToArray();

            var expected = new StringBuilder();
            expected.AppendLine("+- HEIßE.TXT");
            expected.AppendLine("+- Ó.TXT");

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), expected.ToString());
            Assert.AreEqual(generateVdfsEntriesTreeView("Samples/test4.vdf"), expected.ToString());
        }
    }

    class VdfsEntryListBuilder
    {
        public List<VdfsEntry> Entries = new List<VdfsEntry>();

        public VdfsEntryListBuilder Add(string entryName)
        {
            Entries.Add(new VdfsEntry
            {
                Name = entryName,
            });

            return this;
        }

        public VdfsEntryListBuilder AddLast(string entryName)
        {
            Entries.Add(new VdfsEntry
            {
                Name = entryName,
                Type = Vdfs.EntryType.Last,
            });

            return this;
        }

        public VdfsEntryListBuilder AddDirectory(string entryName, uint offset)
        {
            Entries.Add(new VdfsEntry
            {
                Name = entryName,
                Offset = offset,
                Type = Vdfs.EntryType.Directory,
            });

            return this;
        }

        public VdfsEntryListBuilder AddLastDirectory(string entryName, uint offset)
        {
            Entries.Add(new VdfsEntry
            {
                Name = entryName,
                Offset = offset,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
            });

            return this;
        }
    }
}
