using TTSA.Lib;

namespace TTSA.IntegrationTesting;

[Category("Integration Tests")]
public class ASeatingChart
{
    string fileName = "SeatAvailability.dat";

    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    [Test]
    public void ShouldReportThatTheSeatAvailabilityFileIsMissing()
    {
        
        Assert.That(File.Exists(fileName), Is.False);
        var reader = new AvailabilityFileReader(fileName);
        var sut = new SeatingChart(reader);
        FileNotFoundException? ex =
            Assert.Throws<FileNotFoundException>(() => {
                sut.Read();
            });
        Assert.That(ex!.Message, Is.EqualTo("SeatAvailability.dat is missing!"));
    }

    [Test]
    public void ShouldReportThatTheSeatAvailabilityFileIsBadlyFormattedWhenEmpty()
    {
        Assert.That(File.Exists(fileName), Is.False);
        using (StreamWriter writer = File.CreateText(fileName)) { }
        var reader = new AvailabilityFileReader(fileName);
        var sut = new SeatingChart(reader);
        FileFormatException? ex =
            Assert.Throws<FileFormatException>(() => {
                sut.Read();
            });
        Assert.That(ex!.Message, Is.EqualTo("SeatAvailability.dat is badly formatted!"));
        File.Delete(fileName);
    }

    [Test]
    public void ShouldNotReportThatTheSeatAvailabilityFileIsBadlyFormatted()
    {
        // Arrange
        Assert.That(File.Exists(fileName), Is.False);
        using (StreamWriter writer = File.CreateText(fileName)) 
        {
            for(var line = 0; line < 15; line++)
            {
                writer.WriteLine("#####*****#####*****#####*****");
            }
        }
        var reader = new AvailabilityFileReader(fileName);
        var sut = new SeatingChart(reader);
        // Act & Assert
        Assert.DoesNotThrow(() =>
        {
            sut.Read();
        });

        File.Delete(fileName);
    }

    [Test]
    public void ShouldReportThatTheSeatAvailabilityFileIsBadlyFormattedWithBadCharacters()
    {
        // Arrange
        Assert.That(File.Exists(fileName), Is.False);
        using (StreamWriter writer = File.CreateText(fileName))
        {
            for (var line = 0; line < 15; line++)
            {
                writer.WriteLine("#####*****!####*****#####*****");
            }
        }
        var reader = new AvailabilityFileReader(fileName);
        var sut = new SeatingChart(reader);
        // Act & Assert
        var ex = Assert.Throws<FileFormatException>(() =>
        {
            sut.Read();
        });
        Assert.That(ex!.Message, Is.EqualTo("SeatAvailability.dat is badly formatted!"));
        File.Delete(fileName);
    }
}