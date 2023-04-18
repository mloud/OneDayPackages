using System;
using System.Diagnostics;
using System.IO;
using OneDay.PackageTools.Editor;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PackageTools.Editor
{
    public static class PackagePublisher
    {
        public static void Publish(PackageModel packageModel)
        {
            var fullPackagePath= Path.GetFullPath($"Packages/{packageModel.name}/package.json");
            
            // var fullPackagePath = Path.Join(
            //     Application.dataPath.Replace("Assets", ""), 
            //     AssetDatabase.GetAssetPath(packageModel.source));
            fullPackagePath = fullPackagePath.Replace(Path.GetFileName(fullPackagePath), "");
            
            Debug.Log($"Publishing from {fullPackagePath}");
            
            //string fullPath = Path.Combine(Environment.CurrentDirectory, "/YourSubDirectory/yourprogram.exe");
            var psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                UseShellExecute = false,
                WorkingDirectory = fullPackagePath,
                RedirectStandardOutput = true,
                RedirectStandardError = false
            };
            var pNpmRunDist = Process.Start(psiNpmRunDist);
            pNpmRunDist.StandardInput.WriteLine("npm publish --registry http://localhost:4873/");
          
            
            // var npmPath = "C:\\Users\\mloud\\AppData\\Roaming\\npm\\node_modules\\npm\\bin\\npm";
            // ProcessStartInfo startInfo = new ProcessStartInfo(npmPath);
            // startInfo.WindowStyle = ProcessWindowStyle.Normal;
            // startInfo.Arguments = $"publish {fullPackagePath} --registry http://localhost:4873/";
            // startInfo.UseShellExecute = false;
            // startInfo.RedirectStandardOutput = true;
            // startInfo.RedirectStandardError = true;
            //
            // var p = Process.Start(startInfo);
            // p.WaitForExit();
            // string output = p.StandardOutput.ReadToEnd();
            // Debug.Log(output);
        }
    }
}