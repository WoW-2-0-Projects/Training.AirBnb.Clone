using AirBnB.Application.StorageFiles.Brokers;

namespace AirBnB.Infrastructure.StorageFiles.Brokers;

/// <summary>
/// Implementation of the <see cref="IFileBroker"/> interface for file manipulation and transfer operations.
/// </summary>
public class FileBroker : IFileBroker
{
    public FileStream CreateFileStream(string path) => new(path, FileMode.Create);

    public void StreamTransfer(Stream source, Stream destination) => source.CopyTo(destination);

    public void DeleteFile(string path) => File.Delete(path);
    
    public bool FileExists(string path) => File.Exists(path);
}