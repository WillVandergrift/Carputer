using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CarPC.Audio;
using CarPC.FileSys;

namespace CarViewModel.Browser
{
    public class FileBrowserVM : IDisposable
    {
        /// <summary>
        /// Returns a filtered list of files and folders in the specified
        /// </summary>
        /// <param name="folder">The full folder path</param>
        /// <returns></returns>
        public List<fso> GetFilesInFolder(string folder, fso parent, List<string> audioFormats, List<string> videoFormats)
        {
            List<fso> files = new List<fso>();

            foreach (string d in Directory.GetDirectories(folder))
            {
                files.Add(new Folder(d, parent));
            }

            //Add supported files to our list
            foreach (string f in Directory.GetFiles(folder, "*"))
            {
                if (audioFormats.Contains(FileExtension(f)))
                {
                    files.Add(new MusicFile(f, parent));
                }
                else if (videoFormats.Contains(FileExtension(f)))
                {
                    files.Add(new VideoFile(f, parent));
                }
            }

            return files;
        }

        private string FileExtension(string filename)
        {
            return filename.Substring(filename.LastIndexOf(".") + 1);
        }

        /// <summary>
        /// Get all media sources that match the Source Type
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        public List<MediaSource> GetMediaSources(SrcType sourceType, List<MediaSource> sources)
        {
            List<MediaSource> lstSources = sources.Where(s => s.SourceType == sourceType).ToList();

            return lstSources;
        }

        public void Dispose()
        {
            
        }
    }
}
