using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Extensions;
using Fedatto.ConfigProvider.WebApi.Extensions;

WebApplication.CreateBuilder(args)
    .AddCompositionRoot()
    .Build()
    .Configure()
    .Run();
