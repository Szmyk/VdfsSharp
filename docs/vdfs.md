> Adapted from [source code](http://www.bendlins.de/nico/gothic2/GothicVDFS-2.6.0.0_src.7z) of GothicVDFS 2.6. Thanks [Nico Bendlin](https://github.com/nicodex) for sharing this! 

# Virtual Disk File System (VDFS)

VDFS archives are similar in principle to TAR and QPAK (Quake 1/2) archives, in
that they are linear collections of non-compressed files. Every file inside
a VDFS archive can have a name up to 64 bytes long (which is generally an
uppercase 7bit ASCII encoded string), and a size up to 4GB (2^32 bytes).

## Header

| Size        | Description              |
|-------------|--------------------------|
| 256 bytes   | Comment on the archive   |
| 16 bytes    | Signature of the archive |
| 4 bytes     | Number of entries        |
| 4 bytes     | Number of files          |
| 4 bytes     | Timestamp                |
| 4 bytes     | Data size                |
| 4 bytes     | Root offset              |
| 4 bytes     | Entry size               |

### Comment

GothicVDFS fills the comment up to 256 bytes with [SUB](https://en.wikipedia.org/wiki/Substitute_character) character.

### Signature

The signature is always `"PSVDSC_V2.00\r\n\r\n"` in case of Gothic 1 archives, or `"PSVDSC_V2.00\n\r\n\r"` in case of Gothic 2 archives.

### Timestamp

The timestamp is given in [MS DOS date time format](http://www.vsft.com/hal/dostime.htm).

### Entry size

Size of entry header - `80` bytes.

## Entries

| Size        | Description              |
|-------------|--------------------------|
| 64 bytes    | Name                     |
| 4 bytes     | Offset                   |
| 4 bytes     | Size                     |
| 4 bytes     | Type                     |
| 4 bytes     | Attributes               |

### Name

GothicVDFS fills the name up to 64 bytes with spaces.

### Offset

If the entry is a directory, it stores first entry index, otherwise it stores the offset at which the first byte of the file is found

### Size

Size in bytes of the entry content

### Type

It's a bitmask: if `0x80000000` is present, then the entry is a directory and all the following entries should be considered inside of it, until an entry of with bit `0x40000000` set is found, which signals the last entry in a directory.

### Attributes

GothicVDFS sets only four attributes: `FILE_ATTRIBUTE_HIDDEN`, `FILE_ATTRIBUTE_READONLY`, `FILE_ATTRIBUTE_ARCHIVE` and `FILE_ATTRIBUTE_SYSTEM`.
