using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace VdfsSharp
{
    /// <summary>
    /// Provides extracting files from VDFS archive.
    /// </summary>
    public class VdfsExtractor
    {
        VdfsReader _vdfsReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="VdfsExtractor"/> class.
        /// </summary>
        /// <param name="vdfFilePath">Path of VDFS archive.</param>
        public VdfsExtractor(string vdfFilePath)
        {
            _vdfsReader = new VdfsReader(vdfFilePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VdfsExtractor"/> class.
        /// </summary>
        /// <param name="vdfsReader"> An instance of <see cref="VdfsReader"/>. </param>
        public VdfsExtractor(VdfsReader vdfsReader)
        {
            _vdfsReader = vdfsReader;
        }

        /// <summary>
        /// Extracts specific file.
        /// </summary>
        public void ExtractFile(string fileName, string outputFile)
        {
            var entry = _vdfsReader.ReadEntries(false).Where(x => x.Name == fileName).First();

            entry.Content = _vdfsReader.ReadEntryContent(entry);

            entry.SaveToFile(outputFile);
        }

        /// <summary>
        /// Extracts all entries to directory.
        /// </summary>
        /// <param name="outputDirectory">Directory to extract files.</param>
        /// <param name="withHierarchy">Specifies whether to extract with directories hierarchy. </param>
        public void ExtractFiles(string outputDirectory, bool withHierarchy)
        {
            var entries = _vdfsReader.ReadEntries(true).ToList();

            if (withHierarchy)
            {
                var tree = new VdfsEntriesTreeGenerator(entries).Generate();

                saveFiles(tree, outputDirectory);
            }
            else
            {
                saveFiles(entries, outputDirectory);
            }                 
        }

        private void saveFiles(List<VdfsEntry> entries, string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            foreach (var entry in entries.Where(x => x.Type.HasFlag(Vdfs.EntryType.Directory) == false))
            {
                var output = Path.Combine(outputDirectory, entry.Name);

                entry.SaveToFile(output);
            }
        }

        private void saveFiles(VdfsEntriesTree tree, string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            foreach (var node in tree.Childrens)
            {
                var output = Path.Combine(outputDirectory, node.Entry.Name);

                if (node.Entry.Type.HasFlag(Vdfs.EntryType.Directory))
                {
                    saveFiles(node, output);
                }
                else
                {
                    node.Entry.SaveToFile(output);
                }
            }
        }
    }
}
