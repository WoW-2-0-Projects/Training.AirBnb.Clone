namespace AirBnB.Application.StorageFiles.Brokers;

/// <summary>
/// Represents an interface that defines operations related to directory manipulation.
/// </summary>
public interface IDirectoryBroker
{
    /// <summary>
    /// Creates a DirectoryInfo for the specified directory path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DirectoryInfo CreateDirectory(string path);
}