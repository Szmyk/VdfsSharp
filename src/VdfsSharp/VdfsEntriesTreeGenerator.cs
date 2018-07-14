using System.Collections.Generic;

namespace VdfsSharp
{
    /// <summary>
    /// Provides <see cref="VdfsEntriesTree"/> generating.
    /// </summary>
    public class VdfsEntriesTreeGenerator
    {
        List<VdfsEntry> _entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="VdfsEntriesTreeGenerator"/> class.
        /// </summary>
        public VdfsEntriesTreeGenerator(List<VdfsEntry> entries)
        {
            _entries = entries;
        }

        /// <summary>
        /// Generates tree.
        /// </summary>
        public VdfsEntriesTree Generate()
        {
            VdfsEntriesTree tree = new VdfsEntriesTree();

            for (int i = 0; i < _entries.Count; i++)
            {
                var node = tree.AddChild(_entries[i]);

                if (_entries[i].Type.HasFlag(Vdfs.EntryType.Directory))
                {                 
                    generateDirectoryTree(_entries[i], _entries, node);
                }

                if (_entries[i].Type.HasFlag(Vdfs.EntryType.Last))
                {
                    break;
                }
            }

            return tree;
        }

        private void generateDirectoryTree(VdfsEntry directory, List<VdfsEntry> entries, VdfsEntriesTree node)
        {
            for (int i = (int)directory.Offset; ; i++)
            {
                var child = node.AddChild(entries[i]);

                if (entries[i].Type.HasFlag(Vdfs.EntryType.Directory))
                {
                    generateDirectoryTree(entries[i], entries, child);
                }

                if (entries[i].Type.HasFlag(Vdfs.EntryType.Last))
                {
                    break;
                }
            }
        }
    }
}
