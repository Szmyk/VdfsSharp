using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Collections.Generic;

namespace VdfsSharp.Tests
{
    [TestClass]
    [DeploymentItem("Samples/test.vdf")]
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
            /*
            +- _WORK
               +- DATA
                  +- SCRIPTS
                  |  +- SCRIPT.D (0 bytes)
                  +- TEXTURES
                     +- TEXTURE.TEX (0 bytes)
            */

            var entries = new VdfsEntryListBuilder()
                /*0*/.AddLastDirectory("_WORK", 1)
                /*1*/.AddLastDirectory("DATA", 2)
                /*2*/.AddDirectory("SCRIPTS", 4)
                /*3*/.AddLastDirectory("TEXTURES", 5)
                /*4*/.AddLast("SCRIPT.D")
                /*5*/.AddLast("TEXTURE.TEX").Entries.ToArray();

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), generateVdfsEntriesTreeView("Samples/test.vdf"));
        }

        [TestMethod]
        public void Generate_Test2()
        {
            /*
            +- 1
            |  +- _WORK1_1
            |  |  +- TEST1_1.TXT (0 bytes)
            |  +- _WORK1_2
            |     +- TEST1_2.TXT (0 bytes)
            +- 2
               +- _WORK2_1
               |  +- TEST2_1.TXT (0 bytes)
               +- _WORK2_2
                  +- TEST2_2.TXT (0 bytes)
            */

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

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), generateVdfsEntriesTreeView("Samples/test2.vdf"));
        }

        [TestMethod]
        public void Generate_Test3()
        {
            /*
            +- DIR
            |  +- FILE2.TXT (0 bytes)
            +- FILE1.TXT (0 bytes)
            */

            var entries = new VdfsEntryListBuilder()
                /*0*/.AddDirectory("DIR", 2)
                /*1*/.AddLast("FILE1.TXT")
                /*2*/.AddLast("FILE2.TXT").Entries.ToArray();

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), generateVdfsEntriesTreeView("Samples/test3.vdf"));
        }

        [TestMethod]
        public void Generate_Test4()
        {
            /*
            +- HEIßE.TXT (0 bytes)          
            +- Ó.TXT (0 bytes)     
            */

            var entries = new VdfsEntryListBuilder()
                /*0*/.Add("HEIßE.TXT")
                /*0*/.AddLast("Ó.TXT").Entries.ToArray();

            Assert.AreEqual(generateVdfsEntriesTreeView(entries), generateVdfsEntriesTreeView("Samples/test4.vdf"));
        }
    }

    class VdfsEntryListBuilder
    {
        public List<VdfsEntry> Entries = new List<VdfsEntry>();

        public VdfsEntryListBuilder Add(string entryName)
        {
            Entries.Add(new VdfsEntry()
            {
                Name = entryName,
            });

            return this;
        }

        public VdfsEntryListBuilder AddLast(string entryName)
        {
            Entries.Add(new VdfsEntry()
            {
                Name = entryName,
                Type = Vdfs.EntryType.Last,
            });

            return this;
        }

        public VdfsEntryListBuilder AddDirectory(string entryName, uint offset)
        {
            Entries.Add(new VdfsEntry()
            {
                Name = entryName,
                Offset = offset,
                Type = Vdfs.EntryType.Directory,
            });

            return this;
        }

        public VdfsEntryListBuilder AddLastDirectory(string entryName, uint offset)
        {
            Entries.Add(new VdfsEntry()
            {
                Name = entryName,
                Offset = offset,
                Type = Vdfs.EntryType.Last | Vdfs.EntryType.Directory,
            });

            return this;
        }
    }
}
