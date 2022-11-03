using System.Text;

namespace TTSA.Lib;

public class AvailabilityFileReader : IReader
{
    public AvailabilityFileReader(string fileName)
    {
        Name = fileName;
    }

    public string Name { get; set; }

    public object Data
    {
        get
        {
            string[] lines = File.ReadAllLines(Name, Encoding.UTF8);
            return string.Join("\r\n", lines) + "\r\n";
        }
    }

    public bool Exists()
    {
        return File.Exists(Name);
    }

    public bool IsBadlyFormatted()
    {
        bool isBadlyFormatted = false;
        if(Exists() == false)
        {
            isBadlyFormatted = true;
        }
        else
        {
            string[] lines = File.ReadAllLines(Name, Encoding.UTF8);
            if(lines.Length != 15)
            {
                isBadlyFormatted = true;
            }
            else
            {
                isBadlyFormatted = lines.Any( line =>
                    line.Length != 30 ||
                    (line.Any(ch => ch != '#' && ch != '*')));
            }
        }
        return isBadlyFormatted;
    }
}