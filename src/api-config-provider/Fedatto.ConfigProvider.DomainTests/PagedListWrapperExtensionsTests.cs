using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.DomainTests;

[TestFixture]
public class PagedListWrapperTests
{
    [Test]
    public void WrapUp_ListaPaginaUnica_RetornaPagedListWrapper()
    {
        // Arrange
        List<int> payload = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        PagedListWrapper<int> result = payload.WrapUp();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<PagedListWrapper<int>>());
            Assert.That(result.Total, Is.EqualTo(payload.Count));
            Assert.That(result.Skip, Is.EqualTo(0));
            Assert.That(result.Limit, Is.EqualTo(null));
            Assert.That(result.Count, Is.EqualTo(payload.Count));
            Assert.That(result.Payload, Is.SameAs(payload));
        });
    }

    [Test]
    public void WrapUp_Skip1PaginaMenorQueRestoDaLista_RetornaPagedListWrapper()
    {
        // Arrange
        List<string> payload = new List<string> { "maçã", "banana", "laranja", "morango" };
        const int skip = 1;
        const int limit = 2;
        const int total = 4;

        // Act
        PagedListWrapper<string> result = payload.WrapUp(skip, limit, total);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<PagedListWrapper<string>>());
            Assert.That(result.Total, Is.EqualTo(total));
            Assert.That(result.Skip, Is.EqualTo(skip));
            Assert.That(result.Limit, Is.EqualTo(limit));
            Assert.That(result.Count, Is.EqualTo(payload.Count));
            Assert.That(result.Payload, Is.SameAs(payload));
        });
    }

    [Test]
    public void WrapUp_PayloadVazio_RetornaPagedListWrapperZerado()
    {
        // Arrange
        IEnumerable<double> emptyPayload = Enumerable.Empty<double>();

        // Act
        PagedListWrapper<double> result = emptyPayload.WrapUp();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<PagedListWrapper<double>>());
            Assert.That(result.Total, Is.EqualTo(0));
            Assert.That(result.Skip, Is.EqualTo(0));
            Assert.That(result.Limit, Is.EqualTo(null));
            Assert.That(result.Count, Is.EqualTo(0));
            Assert.That(result.Payload, Is.SameAs(emptyPayload));
        });
    }

    [Test]
    public void WrapUp_PayloadNulo_RetornaPagedListWrapperZerado()
    {
        // Arrange
        IEnumerable<double>? emptyPayload = null;

        // Act
        PagedListWrapper<double> result = emptyPayload.WrapUp();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<PagedListWrapper<double>>());
            Assert.That(result.Total, Is.EqualTo(0));
            Assert.That(result.Skip, Is.EqualTo(0));
            Assert.That(result.Limit, Is.EqualTo(null));
            Assert.That(result.Count, Is.EqualTo(0));
            Assert.That(result.Payload, Is.SameAs(Array.Empty<double>()));
        });
    }
}
