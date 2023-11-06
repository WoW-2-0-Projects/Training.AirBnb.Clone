namespace Backend_Project.Application.Files.Brokers;

public interface IFileBroker
{
    FileStream CreateFileStream(string path);

    void StreamTransfer(Stream source, Stream destination);

    void DeleteFile(string path);
}