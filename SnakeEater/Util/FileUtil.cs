using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeEater.Util
{
    /// <summary>
    /// File type enum
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// Specify no type
        /// </summary>
        None,

        /// <summary>
        /// Text file type
        /// </summary>
        Text,

        /// <summary>
        /// Excel2007 file type
        /// </summary>
        Excel,

        /// <summary>
        /// Excel97-2003 file type
        /// </summary>
        Excel97,

        /// <summary>
        /// Pdf file type
        /// </summary>
        Pdf,

        /// <summary>
        /// Data file type
        /// </summary>
        Data
    }

    public class FileUtil
    {
        /// <summary>
        /// SaveFileDialog filter. Parrelled with SnakeEater.Util.FileType
        /// </summary>
        private static string[] typeFilter = 
        {
            "File (*.*)|*.*",
            "Text File (*.txt)|*.txt|All files|*.*",
            "Excel File (*.xlsx)|*.xlsx|All files|*.*",
            "Excel 97~2003 File (*.xls)|*.xls|All files|*.*",
            "Pdf File (*.pdf)|*.pdf|All files|*.*",
            "Data File (*.dat)|*.dat|All files|*.*"
        };

        /// <summary>
        /// Open a file save dialog.
        /// </summary>
        /// <param name="type">File type.</param>
        /// <param name="filePath">Selected file path.</param>
        /// <returns>True if a valid file is selected.</returns>
        public static bool ShowFileSaveDialog(FileType type, out string filePath)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = FileUtil.typeFilter[(int)type];

            DialogResult answer = sfd.ShowDialog();
            if (DialogResult.OK == answer)
            {
                filePath = sfd.FileName;
                return true;
            }
            else
            {
                filePath = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Open a file select dialog.
        /// </summary>
        /// <param name="type">File type.</param>
        /// <param name="filePath">Selected file path.</param>
        /// <returns>True if a valid file is selected.</returns>
        public static bool ShowFileSelectDialog(FileType type, out string filePath)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = FileUtil.typeFilter[(Int32)type];

            DialogResult answer = dialog.ShowDialog();
            if (DialogResult.OK == answer)
            {
                filePath = dialog.FileName;
                return true;
            }
            else
            {
                filePath = string.Empty;
                return false;
            }
        }
    }
}
