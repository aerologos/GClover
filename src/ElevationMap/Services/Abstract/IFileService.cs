using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ElevationMap.Services.Abstract
{
    public interface IFileService
    {
        public Window Target { get; set; }

        public Task<IReadOnlyList<IStorageFile>> OpenFilesAsync(FilePickerOpenOptions options);
        
        public Task<IStorageFile> SaveFileAsync(FilePickerSaveOptions options);
    }
}