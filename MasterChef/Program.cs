using MasterChef.Log;
using MasterChef.Contracts.Services;
using MasterChef.Services;

var builder = WebApplication.CreateBuilder(args);

LogService.Configure(builder.Host);
try
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddMemoryCache();

    builder.Services.AddScoped<ITokenService, TokenService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
finally
{
    LogService.CloseAndFlush();
}
