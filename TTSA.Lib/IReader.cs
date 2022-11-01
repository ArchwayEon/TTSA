namespace TTSA.Lib;

public interface IReader
{
    string Name { get; set; }

    bool Exists();
    bool IsBadlyFormatted();
}