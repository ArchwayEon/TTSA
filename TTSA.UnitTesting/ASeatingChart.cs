using Moq;
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
}