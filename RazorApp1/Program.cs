
using EmailSenderWebApi.Domain.DomainEvents.EventConsumers;
using EmailSenderWebApi.Models.EmailModels;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RazorApp1.Services.EmailService;
using RazorApp1.Services.EmailService.ServiseIntefaces;
using RazorApp1.Services.HostedServices;

using Serilog;
using Serilog.Events;

#pragma warning disable U2U1001 // Stateless classes can be static
Log.Logger=new LoggerConfiguration ( )
   .WriteTo.Console ( )
   .CreateBootstrapLogger ( ); //��������, ��� ���������� ����� ����� ������� �� ������� �� Host.UseSerilog
#pragma warning restore U2U1001 // Stateless classes can be static
Log.Information ("Starting up");

try
{
    #region Builder
    var builder = WebApplication.CreateBuilder (args);

    builder.Host.UseSerilog (( _, conf ) => conf.WriteTo.Console ( ));

    builder.Host.UseSerilog (( ctx, conf ) =>
    {
        conf
            .MinimumLevel.Debug ( ) //<- ����������� ������� ��� ���� ����������
            .WriteTo.File ("log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console (restrictedToMinimumLevel: LogEventLevel.Information)
            .ReadFrom.Configuration (ctx.Configuration)
        ;
    });


    builder.Services.Configure<SmtpCredentions> (
        builder.Configuration.GetSection ("SmtpCredentions"));
    builder.Services.Configure<EmailCredentions> (
        builder.Configuration.GetSection ("EmailCredentions"));

    builder.Services.Configure<OptionsEmailSender> (builder.Configuration.GetSection ("OptionsEmailDataEvent"));
      
    // Add services to the container.
    builder.Services.AddSingleton<IEmailSender, EmailSenderService> ( );

    builder.Services.AddHostedService<SendMailBackgroundService> ( );

    builder.Services.AddHostedService<SenderProductChangedEvent> ( );


    builder.Services.AddControllersWithViews ( );

    var app = builder.Build ( );
    #endregion

    #region Configure
    app.UseStaticFiles ( );
    app.UseSerilogRequestLogging ( );

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment ( ))
    {
        app.UseExceptionHandler ("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ( );
    }

    app.UseHttpsRedirection ( );
    app.UseStaticFiles ( );

    app.UseRouting ( );

    app.UseAuthorization ( );

    app.MapControllerRoute (
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.UseSerilogRequestLogging ( );
    #endregion

    app.Run ( );
}
catch (Exception E)
{
    Log.Fatal (E, "������ ������!");
}
finally
{
    Log.Information ("Shut down complete");
    Log.CloseAndFlush ( );
}

