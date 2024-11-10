using BookLab.Application.Interfaces;
using Moq;

namespace BookLab.Tests.Mocks;

public class HashServiceMocks
{
    public Mock<IHashService> HashService { get; set; }

    public HashServiceMocks()
    {
        HashService = new Mock<IHashService>();

        HashService.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string text, string hash) =>
            {
                return text == hash;
            });
    }
}
