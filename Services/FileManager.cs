using Assignment_1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Services
{
    internal interface IFilemanager
    {
        public string ReadFile(string filePath);
        public void SaveFile(string filePath, string content);
    }

    internal class FileManager : IFilemanager
    {
        public string ReadFile(string filePath)
        {
            try
            {
                using var sr = new StreamReader(filePath);
                return sr.ReadToEnd();
            }
            catch
            { }
            return null;
        }

        public void SaveFile(string filePath, string content)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
        }

    }
}

