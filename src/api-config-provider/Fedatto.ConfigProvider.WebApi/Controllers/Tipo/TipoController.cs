using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Tipo;

[Route(Rotas.Tipos)]
public class TipoController : Controller
{
    private readonly ITipoApplication _application;

    public TipoController(
        ITipoApplication application)
    {
        _application = application;
    }
    
    [HttpGet(Rotas.TiposGetTipos)]
    public async Task<ActionResult<PagedListWrapper<GetTipoResponseModel>>> Get_Index(
        [FromQuery(Name = ArgumentosNomeados.IdTipo)] int? id = null,
        [FromQuery(Name = ArgumentosNomeados.Nome)] string? nome = null,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool? habilitado = null,
        [FromQuery(Name = ArgumentosNomeados.Skip)] int? skip = 0,
        [FromQuery(Name = ArgumentosNomeados.Limit)] int? limit = null)
    {
        return Ok((await _application.BuscarTipos(
                id,
                nome,
                habilitado,
                skip,
                limit))
            .Map(tipo => tipo.ToGetResponseModel()));
    }
    
    [HttpGet(Rotas.TiposGetTipo)]
    public async Task<ActionResult<PagedListWrapper<GetTipoResponseModel>>> Get_ById(
        [FromRoute(Name = ArgumentosNomeados.IdTipo)] int id)
    {
        return Ok((await _application.BuscarTipo(id))
            .ToGetResponseModel());
    }
}
