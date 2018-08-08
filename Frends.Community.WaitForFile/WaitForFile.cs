using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.WaitForFile
{ 
/// <summary>
/// Parameters for file appearing
/// </summary>
public class Parameters
{
    /// <summary>
    /// File path
    /// </summary>
    [DefaultValue(@"C:\")]
    [DisplayName(@"File path")]
    [DisplayFormat(DataFormatString = "Text")]
    public string FilePath { get; set; }
    /// <summary>
    /// File mask
    /// </summary>
    [DefaultValue(@"*.*")]
    [DisplayName(@"File mask")]
    [DisplayFormat(DataFormatString = "Text")]
    public string FileMask { get; set; }
    /// <summary>
    /// Time out in milliseconds
    /// </summary>
    [DefaultValue(@"3000")]
    [DisplayName(@"Timeout (ms)")]
    [DisplayFormat(DataFormatString = "Text")]
    public int TimeoutMS { get; set; }
    /// <summary>
    /// Continue if file allready exists. If false, file action is being waited even if file matching the file mask exist.
    /// </summary>
    [DefaultValue(@"true")]
    [DisplayName(@"Continue if file allready exists")]
    public bool ContinueIfExists { get; set; }
}

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

    /// <summary>
     /// SubmitForm
     /// </summary>
    public static class WaitFile
    {
        /// <summary>
        /// Wait for file to appear or to be modified. For a more detailed documentation see: https://github.com/CommunityHiQ/Frends.Community.WaitForFile
        /// </summary>
        /// <returns>Object {string FilePath, bool FileExists }  </returns>
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
