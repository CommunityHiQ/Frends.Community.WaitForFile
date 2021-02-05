using System.ComponentModel;
using System.IO;
using System.Threading;
using Microsoft.CSharp; // You can remove this if you don't need dynamic type in .NET Standard frends Tasks

#pragma warning disable 1591

namespace Frends.Community.WaitForFile
{

    /// <summary>
    /// Return object
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Result path
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// File allready exists or not (it was created while task was waiting)
        /// </summary>
        public bool FileExists { get; set; }
    }

    public static class WaitFile
    {
        /// <summary>
        /// Wait for file to appear or to be modified. For a more detailed documentation see: https://github.com/CommunityHiQ/Frends.Community.WaitForFile
        /// </summary>
        /// <returns>Object {string FilePath, bool FileExists }  </returns>
        /// 
        public static Output WaitForFileToAppear(Parameters parameters)
        {
            if (parameters.ContinueIfExists && File.Exists(Path.Combine(parameters.FilePath, parameters.FileMask)))
            {
                return new Output { FileExists = true };
            }

            var fileWatcher = new FileSystemWatcher
            {
                Path = parameters.FilePath,
                Filter = parameters.FileMask
            };

            var path = "";
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fileWatcher.Changed += new FileSystemEventHandler((s, e) => path = e.FullPath);
            fileWatcher.Deleted += new FileSystemEventHandler((s, e) => path = e.FullPath);
            fileWatcher.Created += new FileSystemEventHandler((s, e) => path = e.FullPath);
            fileWatcher.Renamed += new RenamedEventHandler((s, e) => path = e.FullPath);
            fileWatcher.EnableRaisingEvents = true;

            fileWatcher.WaitForChanged(WatcherChangeTypes.All, parameters.TimeoutMS);

            return new Output { FilePath = path };
        }
    }
}
