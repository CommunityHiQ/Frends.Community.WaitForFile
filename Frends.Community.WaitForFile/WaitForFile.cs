using System.IO;
using Frends.Tasks.Attributes;
using System.ComponentModel;

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
    [DefaultDisplayType(DisplayType.Text)]
    public string FilePath { get; set; }
    /// <summary>
    /// File mask
    /// </summary>
    [DefaultValue(@"*.*")]
    [DisplayName(@"File mask")]
    [DefaultDisplayType(DisplayType.Text)]
    public string FileMask { get; set; }
    /// <summary>
    /// Time out in milliseconds
    /// </summary>
    [DefaultValue(@"3000")]
    [DisplayName(@"Timeout (ms)")]
    [DefaultDisplayType(DisplayType.Text)]
    public int TimeoutMS { get; set; }
    /// <summary>
    /// Continut if file exists
    /// </summary>
    [DefaultValue(@"true")]
    [DisplayName(@"Continue if exists")]
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
    /// File exists
    /// </summary>
    public bool FileExists { get; set; }
}

    /// <summary>
     /// SubmitForm
     /// </summary>
    public static class WaitFile
    {
        /// <summary>
        /// Wait for file to appear.
        /// </summary>
        /// <returns>Object {string FilePath }  </returns>
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
