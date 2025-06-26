using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Files
{
    static class FileHelper
    {
        public static bool CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log($"文件缺失：{path}");
                return false;
            }

            return true;
        }
    }
}
