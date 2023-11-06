namespace Backend_Project.Application.Files.Brokers;

public interface IDirectoryBroker
{
    DirectoryInfo CreateDirectory(string path);

    void DeleteDirectory(string path, bool deleteSubDirectoriesAndFiles = false);

    IEnumerable<string> EnumerateFiles(string path);
}