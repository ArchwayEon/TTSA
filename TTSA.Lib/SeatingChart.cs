using System.Data;

namespace TTSA.Lib;

public class SeatingChart
{
    private readonly IReader _reader;
    private string _chart;

    public SeatingChart(IReader reader)
    {
        _reader = reader;
        _chart = "";
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

        _chart = (string)_reader.Data;
        
    }

    public override string ToString()
    {
        return _chart;
    }
}