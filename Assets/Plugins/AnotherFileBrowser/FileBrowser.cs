#if UNITY_STANDALONE_WIN
using Ookii.Dialogs;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AnotherFileBrowser.Windows
{
    public static class FileBrowser
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        /// <summary>
        /// FileDialog for picking a single file
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="filepath">User picked path (Callback)</param>
        public static void OpenFileBrowser(BrowserProperties browserProperties, Action<string> filepath)
        {
            var openFileDialog = new VistaOpenFileDialog
            {
                Multiselect = false,
                Title = browserProperties.Title ?? "Select a File",
                InitialDirectory = browserProperties.InitialDirectory ?? @"C:\",
                Filter = browserProperties.Filter ?? "All files (*.*)|*.*",
                FilterIndex = browserProperties.FilterIndex + 1,
                RestoreDirectory = browserProperties.RestoreDirectory
            };

            if (openFileDialog.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK) 
                filepath(openFileDialog.FileName);
        }

        /// <summary>
        /// FileDialog for picking multiple file(s)
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="filepath">User picked path(s) (Callback)</param>
        public static void OpenMultiSelectFileBrowser(BrowserProperties browserProperties, Action<string[]> filepath)
        {
            var openFileDialog = new VistaOpenFileDialog
            {
                Multiselect = true,
                Title = browserProperties.Title ?? "Select a File",
                InitialDirectory = browserProperties.InitialDirectory ?? @"C:\",
                Filter = browserProperties.Filter ?? "All files (*.*)|*.*",
                FilterIndex = browserProperties.FilterIndex + 1,
                RestoreDirectory = browserProperties.RestoreDirectory
            };

            if (openFileDialog.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK) 
                filepath(openFileDialog.FileNames);
        }

        /// <summary>
        /// FileDialog for selecting any folder 
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="folderpath">User picked path(s) (Callback)</param>
        public static void OpenFolderBrowser(BrowserProperties browserProperties, Action<string> folderpath)
        {
            var openFolderDialog = new VistaFolderBrowserDialog
            {
                Description = browserProperties.Title, 
                UseDescriptionForTitle = true
            };

            if (openFolderDialog.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK) 
                folderpath(openFolderDialog.SelectedPath);
        }

        /// <summary>
        /// FileDialog for saving any file, returns path with extension for further uses
        /// </summary>
        /// <param name="browserProperties">Special Properties of File Dialog</param>
        /// <param name="defaultFileName">Default File Name</param>
        /// <param name="defaultExt">Default File name extension, adds after default file name.</param>
        /// <param name="savepath">User picked path(s) (Callback)</param>
        public static void SaveFileBrowser(BrowserProperties browserProperties, string defaultFileName, string defaultExt, Action<string> savepath)
        {
            var saveFileDialog = new VistaSaveFileDialog
            {
                FileName = defaultFileName,
                DefaultExt = defaultExt,
                CheckPathExists = true,
                OverwritePrompt = true,
                Title = browserProperties.Title,
                InitialDirectory = browserProperties.InitialDirectory ?? @"C:\",
                Filter = browserProperties.Filter,
                FilterIndex = browserProperties.FilterIndex + 1,
                RestoreDirectory = browserProperties.RestoreDirectory
            };

            if (saveFileDialog.ShowDialog(new WindowWrapper(GetActiveWindow())) == DialogResult.OK) 
                savepath(saveFileDialog.FileName);
        }
    }

    public sealed class BrowserProperties
    {
        public string Title { get; set; } 
        public string InitialDirectory { get; set; }
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public bool RestoreDirectory { get; set; } = true;

        public BrowserProperties() { }
        public BrowserProperties(string title) { Title = title; }
    }
    
    public sealed class WindowWrapper : IWin32Window
    {
        public IntPtr Handle { get; }

        public WindowWrapper(IntPtr handle) => Handle = handle;
    }
}
#endif