using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChave
{
    int Id { get; }
    IAplicacao Aplicacao { get; }
    string Nome { get; }
    ITipo Tipo { get; }
    bool Lista { get; }
    bool PermiteNulo { get; }
    int? IdChavePai { get; }
    bool Habilitado { get; }
    DateTime? VigenteDe { get; }
    DateTime? VigenteAte { get; }
}
