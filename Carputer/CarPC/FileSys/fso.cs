using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarPC.FileSys
{
    public enum fsoType
    {
        File,
        Folder,
        Music,
        Video,
        Playlist,
        Source
    }

    /// <summary>
    /// File System Object
    /// </summary>
    public interface fso
    {
        string Name { get; set;}
        string Path { get; set; }
        fso Parent { get; set; }
        ImageSource Thumbnail { get; set; }
        fsoType FileType { get; set; }
    }
}
