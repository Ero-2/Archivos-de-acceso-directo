using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Archivos_de_acceso_directo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Rutas de archivos
            string targetPath = @"C:\Users\erick garcia\Desktop:\Archivo.exe";
            string shortcutPath = @"C:\Users\erick garcia\Desktop\AccesoDirecto.lnk";

            // Crear un acceso directo
            CreateShortcut(targetPath, shortcutPath);

            // Leer información del acceso directo
            string targetPathFromShortcut = GetShortcutTargetPath(shortcutPath);
            Console.WriteLine($"El acceso directo apunta a: {targetPathFromShortcut}");

            // Modificar el destino del acceso directo
            string newTargetPath = @"C:\Ruta\Al\NuevoArchivo.exe";
            UpdateShortcutTarget(shortcutPath, newTargetPath);

            // Eliminar el acceso directo
            DeleteShortcut(shortcutPath);

            Console.ReadKey();
        }

        static void CreateShortcut(string targetPath, string shortcutPath)
        {
            using (StreamWriter writer = new StreamWriter(shortcutPath))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + targetPath.Replace('\\', '/'));
                writer.Flush();
            }
        }

        static string GetShortcutTargetPath(string shortcutPath)
        {
            string targetPath = null;
            using (StreamReader reader = new StreamReader(shortcutPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.StartsWith("URL="))
                    {
                        targetPath = line.Substring(4).Replace('/', '\\');
                        break;
                    }
                }
            }
            return targetPath;
        }

        static void UpdateShortcutTarget(string shortcutPath, string newTargetPath)
        {
            // Modificar el destino del acceso directo
            using (StreamWriter writer = new StreamWriter(shortcutPath))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + newTargetPath.Replace('\\', '/'));
                writer.Flush();
            }
        }

        static void DeleteShortcut(string shortcutPath)
        {
            // Eliminar el acceso directo
            File.Delete(shortcutPath);
        }

    }
}
