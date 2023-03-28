using System;
using System.IO;

namespace N225.Domain.Modules.Utils
{
    /// <summary>
    /// Directory クラスに関する汎用関数を管理するクラス
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        /// 指定したパスにディレクトリが存在しない場合
        /// すべてのディレクトリとサブディレクトリを作成します
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo SafeCreateDirectory(string path = "N225/Csvfile/")
        {
            string dir = LocalApplicationDataPath();
            
            dir = dir + "/" + path;

            if (Directory.Exists(dir))
            {
                return null;
            }
            return Directory.CreateDirectory(dir);
        }
        /// <summary>
        /// C:\ProgramData path 取得
        /// </summary>
        /// <returns></returns>
        public static string ProgramDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).Replace("\\", "/"); ;
        }
        /// <summary>
        /// C:\User\Adminstrator\Documents 
        /// </summary>
        /// <returns></returns>
        public static string MyDocumentsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/"); ;
        }
        /// <summary>
        /// C:\User\Adminstrator\\AppData\Local
        /// </summary>
        /// <returns></returns>
        public static string LocalApplicationDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace("\\", "/"); ;
        }
    }
}

    

