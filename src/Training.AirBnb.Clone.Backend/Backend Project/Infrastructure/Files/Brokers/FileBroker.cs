using Backend_Project.Application.Files.Brokers;

namespace Backend_Project.Infrastructure.Files.Brokers;

public class FileBroker : IFileBroker
{
    public FileStream CreateFileStream(string path) => new(path, FileMode.Create);

    public void StreamTransfer(Stream source, Stream destination) => source.CopyTo(destination);

    public void DeleteFile(string path) => File.Delete(path);
}