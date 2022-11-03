namespace TTSA.Lib;

public interface IReader
{
    string Name { get; set; }
    object Data { get; }

    bool Exists();
    bool IsBadlyFormatted();
}