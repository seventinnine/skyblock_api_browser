using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Skyblock.Common.Helper
{
    public static class AppDataIO
    {
        private static readonly string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private const string appDataFolderName = "HypixelSkyblockAPIBrowser";

        public static async Task<bool> TryWriteTextAsync(string filePath, string contents)
        {
            try
            {
                TryCreateDirectory(appDataFolderName);
                await File.WriteAllTextAsync(Path.Combine(appDataDirectory, appDataFolderName, filePath), contents);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AppDataIO.Write ({ex.StackTrace})");
                return false;
            }
        }
        public static bool TryWriteText(string filePath, string contents)
        {
            try
            {
                TryCreateDirectory(appDataFolderName);
                File.WriteAllText(Path.Combine(appDataDirectory, appDataFolderName, filePath), contents);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AppDataIO.Write ({ex.StackTrace})");
                return false;
            }
        }

        public static async Task<string?> TryReadTextAsync(string filePath)
        {
            try
            {
                return await File.ReadAllTextAsync(Path.Combine(appDataDirectory, appDataFolderName, filePath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AppDataIO.Write ({ex.StackTrace})");
                return null;
            }
        }

        public static string? TryReadText(string filePath)
        {
            try
            {
                return File.ReadAllText(Path.Combine(appDataDirectory, appDataFolderName, filePath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AppDataIO.Read ({ex.StackTrace})");
                return null;
            }
        }

        private static void TryCreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(Path.Combine(appDataDirectory, directoryPath));
        }


    }
}
