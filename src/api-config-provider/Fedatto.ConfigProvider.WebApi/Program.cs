using Fedatto.ConfigProvider.CrossCutting.CompositionRoot;
using Fedatto.ConfigProvider.WebApi.Extensions;

WebApplication.CreateBuilder(args)
    .AddCompositionRoot()
    .Build()
    .Configure()
    .Run();
