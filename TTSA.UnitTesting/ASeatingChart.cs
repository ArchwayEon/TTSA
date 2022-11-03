using Moq;
using System.Text;
using TTSA.Lib;

namespace TTSA.UnitTesting;

[Category("Unit Tests")]
public class ASeatingChart
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldReportThatTheSeatAvailabilityFileIsMissing()
    {
        var mockReader = new Mock<IReader>();
        mockReader.Setup(x => x.Name).Returns("SeatAvailability.dat");
        mockReader.Setup(x => x.Exists()).Returns(false);
        var sut = new SeatingChart(mockReader.Object);
        FileNotFoundException? ex =
            Assert.Throws<FileNotFoundException>(() => {
                sut.Read();
        });
        Assert.That(ex!.Message, Is.EqualTo("SeatAvailability.dat is missing!"));
    }

    [Test]
    public void ShouldReportThatTheSeatAvailabilityFileIsBadlyFormatted()
    {
        var mockReader = new Mock<IReader>();
        mockReader.Setup(x => x.Name).Returns("SeatAvailability.dat");
        mockReader.Setup(x => x.Exists()).Returns(true);
        mockReader.Setup(x => x.IsBadlyFormatted()).Returns(true);
        var sut = new SeatingChart(mockReader.Object);
        FileFormatException? ex =
            Assert.Throws<FileFormatException>(() => {
                sut.Read();
            });
        Assert.That(ex!.Message, Is.EqualTo("SeatAvailability.dat is badly formatted!"));
    }

    [Category("Happy Path")]
    [Test]
    public void ShouldReportTheSeatingChart()
    {
        var mockReader = new Mock<IReader>();
        mockReader.Setup(x => x.Name).Returns("SeatAvailability.dat");
        mockReader.Setup(x => x.Exists()).Returns(true);
        mockReader.Setup(x => x.IsBadlyFormatted()).Returns(false);
        var stringBuilder = new StringBuilder();
        for(var i = 0; i < 15; i++)
        {
            stringBuilder.AppendLine("#####*****#####*****#####*****");
        }
        mockReader.Setup(x => x.Data).Returns(stringBuilder.ToString());
        var sut = new SeatingChart(mockReader.Object);
        sut.Read();
        Assert.That(sut.ToString(), Is.EqualTo(stringBuilder.ToString()));
    }
}