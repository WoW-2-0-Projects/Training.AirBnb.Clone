using AirBnB.Domain.Entities.StorageFiles;
using AirBnB.Infrastructure.StorageFiles.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.StorageFiles.Mappers;

public class StorageFileToUrlConverter(IOptions<StorageFileSettings> storageFileSettings, IOptions<ApiSettings> apiSettings)
    : IValueConverter<StorageFile, string>
{
    public string Convert(StorageFile sourceMember, ResolutionContext context)
    {
        // Get relative path
        var relativePath = Path.Combine(
            storageFileSettings.Value.LocationSettings.First(x => x.StorageFileType == sourceMember.Type).FolderPath,
            sourceMember.FileName
        );

        // Get absolute url
        var absoluteUrl = new Uri(new Uri(apiSettings.Value.BaseAddress), relativePath);
        return absoluteUrl.ToString();
    }
}