using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using IndoorMap.Services.Abstract;

namespace IndoorMap.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Gets or sets the target window for dialogs.
        /// Implemented via property injection due to complexity of initialization process.
        /// </summary>
        public Window Target { get; set; }

        public Task<IReadOnlyList<IStorageFile>> OpenFilesAsync(FilePickerOpenOptions options)
        {
            if (Target == null) return Task.FromResult<IReadOnlyList<IStorageFile>>(null);

            return Target.StorageProvider.OpenFilePickerAsync(options);
        }

        public Task<IStorageFile> SaveFileAsync(FilePickerSaveOptions options)
        {
            if (Target == null) return Task.FromResult<IStorageFile> (null);

            return Target.StorageProvider.SaveFilePickerAsync(options);
        }
    }
}