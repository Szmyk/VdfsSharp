using System;

namespace VdfsSharp
{
    /// <summary>
    /// Represents VDFS archive header.
    /// </summary>
    public class VdfsHeader
    {
        /// <summary>
        /// Gets or sets the name of entry.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or set the signature of archibe.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or set the number of all entries in archive.
        /// </summary>
        public uint EntryCount { get; set; }

        /// <summary>
        /// Gets or set the number of files entries in archive.
        /// </summary>
        public uint FileCount { get; set; }

        /// <summary>
        /// Gets or set the time in which the archive was built.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or set the size of archive.
        /// </summary>
        public uint DataSize { get; set; }

        /// <summary>
        /// Gets or set the offset at which entries table starts.
        /// </summary>
        public uint RootOffset { get; set; }

        /// <summary>
        /// Gets or set the size in bytes of entry header.
        /// </summary>
        public int EntrySize { get; set; }
    }
}
