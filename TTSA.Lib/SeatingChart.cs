using System.Data;

namespace TTSA.Lib;

public class SeatingChart
{
    private readonly IReader _reader;

    public SeatingChart(IReader reader)
    {
        _reader = reader;
    }

    public void Read()
    {
        if (_reader.Exists() == false)
        {
            throw new FileNotFoundException($"{_reader.Name} is missing!");
        }
        if (_reader.IsBadlyFormatted())
        {
            throw new FileFormatException($"{_reader.Name} is badly formatted!");
        }
        
    }
}