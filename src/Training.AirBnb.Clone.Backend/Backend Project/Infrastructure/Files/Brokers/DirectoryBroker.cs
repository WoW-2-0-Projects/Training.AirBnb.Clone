using Backend_Project.Application.Files.Brokers;

namespace Backend_Project.Infrastructure.Files.Brokers;

public class DirectoryBroker : IDirectoryBroker
{
    public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);

    public void DeleteDirectory(string path, bool deleteSubDirectoriesAndFiles = false) => Directory.Delete(path, deleteSubDirectoriesAndFiles);

    public IEnumerable<string> EnumerateFiles(string path) => Directory.EnumerateFiles(path);
}