using AirBnB.Application.StorageFiles.Brokers;

namespace AirBnB.Infrastructure.StorageFiles.Brokers;

/// <summary>
/// Implementation of the <see cref="IDirectoryBroker"/> interface for directory-related operations.
/// </summary>
public class DirectoryBroker : IDirectoryBroker
{
    public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);
}