#pragma warning disable 1591

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
        /// Timeout in milliseconds
        /// </summary>
        [DefaultValue(@"3000")]
        [DisplayName(@"Timeout (ms)")]
        public int TimeoutMS { get; set; }
        /// <summary>
        /// Continue if file already exists. If false, file action is being waited even if file matching the file mask exist.
        /// </summary>
        [DefaultValue(@"true")]
        [DisplayName(@"Continue if file allready exists")]
        public bool ContinueIfExists { get; set; }
    }

}
