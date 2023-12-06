using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.WebApi.Controllers.Aplicacao;

namespace Fedatto.ConfigProvider.WebApiTests;

public class AplicacaoModelExtensionsTests
{
    [Test]
    public void ToGetResponseModel_DadosCheios_DeveMapearCorretamente()
    {
        // Arrange
        IAplicacao aplicacao = new Aplicacao
        {
            AppId = Guid.NewGuid(),
            Nome = "TesteApp",
            Sigla = "TA",
            Aka = "AppAlias",
            Habilitado = true,
            VigenteDe = DateTime.Now,
            VigenteAte = DateTime.Now.AddYears(1)
        };

        // Act
        GetAplicacaoResponseModel result = aplicacao.ToGetResponseModel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.AppId, Is.EqualTo(aplicacao.AppId));
            Assert.That(result.Nome, Is.EqualTo(aplicacao.Nome));
            Assert.That(result.Sigla, Is.EqualTo(aplicacao.Sigla));
            Assert.That(result.Aka, Is.EqualTo(aplicacao.Aka));
            Assert.That(result.Habilitado, Is.EqualTo(aplicacao.Habilitado));
            Assert.That(result.VigenteDe, Is.EqualTo(aplicacao.VigenteDe));
            Assert.That(result.VigenteAte, Is.EqualTo(aplicacao.VigenteAte));
        });
    }
    
    [Test]
    public void ToGetResponseModel_ApenasDadosObrigatorios_DeveMapearCorretamente()
    {
        // Arrange
        IAplicacao aplicacao = new Aplicacao
        {
            Nome = "TesteApp",
            Sigla = "TA",
            Habilitado = true,
            VigenteDe = DateTime.Now,
            VigenteAte = DateTime.Now.AddYears(1)
        };

        // Act
        GetAplicacaoResponseModel result = aplicacao.ToGetResponseModel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.AppId, Is.InstanceOf<Guid>());
            Assert.That(result.Nome, Is.EqualTo(aplicacao.Nome));
            Assert.That(result.Sigla, Is.EqualTo(aplicacao.Sigla));
            Assert.That(result.Aka, Is.Null);
            Assert.That(result.Habilitado, Is.EqualTo(aplicacao.Habilitado));
            Assert.That(result.VigenteDe, Is.EqualTo(aplicacao.VigenteDe));
            Assert.That(result.VigenteAte, Is.EqualTo(aplicacao.VigenteAte));
        });
    }
    
    [Test]
    public void ToEntity_RequestModelCompleto_DeveMapearCorretamente()
    {
        // Arrange
        var factory = new AplicacaoFactory();
        var requestModel = new PostAplicacaoRequestModel
        {
            AppId = Guid.NewGuid(),
            Nome = "TesteApp",
            Sigla = "TA",
            Aka = "AppAlias",
            Habilitado = true,
            VigenteDe = DateTime.Now,
            VigenteAte = DateTime.Now.AddYears(1)
        };

        // Act
        IAplicacao result = factory.ToEntity(requestModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(requestModel.AppId ?? result.AppId, Is.EqualTo(result.AppId));
            Assert.That(requestModel.Nome, Is.EqualTo(result.Nome));
            Assert.That(requestModel.Sigla, Is.EqualTo(result.Sigla));
            Assert.That(requestModel.Aka, Is.EqualTo(result.Aka));
            Assert.That(requestModel.Habilitado, Is.EqualTo(result.Habilitado));
            Assert.That(requestModel.VigenteDe, Is.EqualTo(result.VigenteDe));
            Assert.That(requestModel.VigenteAte, Is.EqualTo(result.VigenteAte));
        });
    }

    [Test]
    public void ToEntity_RequestModelSemAppId_DeveGerarEntidadeComNovoAppId()
    {
        // Arrange
        var factory = new AplicacaoFactory();
        var requestModel = new PostAplicacaoRequestModel
        {
            Nome = "TesteApp",
            Sigla = "TA",
            Habilitado = true,
            VigenteDe = DateTime.Now,
            VigenteAte = DateTime.Now.AddYears(1)
        };

        // Act
        IAplicacao result = factory.ToEntity(requestModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AppId, Is.Not.EqualTo(Guid.Empty));
        });
    }
}

file record Aplicacao : IAplicacao
{
    public Guid AppId { get; init; }
    public required string Nome { get; init; }
    public required string Sigla { get; init; }
    public string? Aka { get; init; }
    public bool Habilitado { get; init; }
    public DateTime VigenteDe { get; init; }
    public DateTime VigenteAte { get; init; }
}
