namespace AirBnB.Application.StorageFiles.Brokers;

/// <summary>
/// This interface defines operations related to file manipulation and transfer.
/// </summary>
public interface IFileBroker
{
    /// <summary>
    ///  Creates a FileStream for the specified file path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    FileStream CreateFileStream(string path);

    /// <summary>
    /// Transfers data from the source stream to the destination stream.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    void StreamTransfer(Stream source, Stream destination);

    /// <summary>
    /// Deletes the file at the specified path.
    /// </summary>
    /// <param name="path"></param>
    void DeleteFile(string path);

    /// <summary>
    /// Checks if the file at the specified path exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    bool FileExists(string path);
}