using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Core
{
    public static class FileUtils
    {
        public static readonly int FileAlreadyExistsError = 0;
        public static readonly int SaveOperationError = 1;
        public static readonly int FileDoesNotExistError = 2;
        public static readonly int DeleteOperationError = 3;
        public static readonly int ReadingFileOperationError = 4;
        public static readonly int DirectoryDoesNotExistError = 5;
        public static readonly int GetFilesError = 6;
        
        public static async UniTask<DataResult<string>> SaveToFile(string filename, string path, byte[] data, bool overwrite)
        {
            var pathToFile = Path.Combine(path, filename);
            return await SaveToFileOperation(filename, path, overwrite, async () =>
            {
                try
                {
                    await File.WriteAllBytesAsync(pathToFile, data);
                }
                catch (Exception e)
                {
                    return DataResult<string>.WithError(e.Message);    
                }
                return DataResult<string>.WithData(pathToFile);
            });
        }

        public static bool ExistsFile(string filePath) => File.Exists(filePath);
        
        public static async UniTask<DataResult<string>> SaveToFile(string filename, string path, string data, bool overwrite)
        {
            var pathToFile = Path.Combine(path, filename);
            return await SaveToFileOperation(filename, path, overwrite, async () =>
            {
                try
                {
                    await File.WriteAllTextAsync(pathToFile, data);
                }
                catch (Exception e)
                {
                    return DataResult<string>.WithError(e.Message);
                }
                return DataResult<string>.WithData(pathToFile);
            });
        }

        private static async UniTask<DataResult<string>> SaveToFileOperation(
            string filename, string path, bool overwrite,  Func<UniTask<DataResult<string>>> saveOperation)
        {
            var pathToFile = Path.Combine(path, filename);
            try
            {
                CreateDirectoryIfNeeded(path);

                bool fileExists = File.Exists(pathToFile);

                if (fileExists && !overwrite)
                {
                    return DataResult<string>
                        .WithError(FileAlreadyExistsError,$"File at destination {pathToFile} already exists");
                }

                return await saveOperation();
            }
            catch (Exception e)
            {
                Debug.LogError($"Saving file to  {pathToFile} failed with error {e.ToString()}");
                return DataResult<string>.WithError(SaveOperationError, e.Message);
            }
        }

        public static async UniTask<Result> DeleteFile(string fileName, string path) => 
            await DeleteFile(Path.Combine(path, fileName));
        public static async UniTask<Result> DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return Result
                    .WithError(FileDoesNotExistError,$"Deleting file at {filePath} failed with error: File does not exists");
            }
            try
            {
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                return Result
                    .WithError(DeleteOperationError, $"Deleting file at {filePath} failed with error: {e.Message}");
            }
            return Result.WithOk();
        }

        public static async UniTask<DataResult<byte[]>> LoadBinaryData(string filePath,
            CancellationToken cancellationToken)
        {
            if (!File.Exists(filePath))
            {
                return DataResult<byte[]>
                    .WithError(FileDoesNotExistError, $"No such file at {filePath} exists");
            }

            try
            {
                var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
                return DataResult<byte[]>.WithData(bytes);
            }
            catch (Exception e)
            {
                return DataResult<byte[]>
                    .WithError(ReadingFileOperationError,$"Reading file at {filePath} failed with error: {e.Message}");
            }
        }
        public static async UniTask<DataResult<byte[]>> LoadBinaryData(
                string fileName, string path, CancellationToken cancellationToken) =>
            await LoadBinaryData(Path.Combine(path, fileName), cancellationToken);
        
        public static async UniTask<DataResult<string>> LoadFile(string fileName, string path)
        {
            string destination = Path.Combine(path, fileName);

            if (!File.Exists(destination))
            {
                return DataResult<string>
                    .WithError( FileAlreadyExistsError, $"No such file at {path} exists");
            }

            try
            {
                var text = await File.ReadAllTextAsync(destination);
                return DataResult<string>.WithData(text);
            }
            catch (Exception e)
            {
                return DataResult<string>
                    .WithError(ReadingFileOperationError, $"Reading file at {path} failed with error: {e.Message}");
            }
        }

        public static async UniTask<DataResult<string[]>> GetFileNames(string path, string searchPattern)
        {
            if (!Directory.Exists(path)) 
                return DataResult<string[]>
                    .WithError(DirectoryDoesNotExistError, $"Directory {path} not found");
           
            try
            {
                return DataResult<string[]>.WithData(Directory.GetFiles(path, searchPattern));
            }
            catch (Exception e)
            {
                return DataResult<string[]>
                    .WithError(GetFilesError, $"Get file names at {path} failed with error: {e.Message}");
            }
        }

        private static void CreateDirectoryIfNeeded(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}