using ImportExcelDapper.ProgressHub;
using ImportExcelDapper.Services;
using Microsoft.Identity.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//---------CODETHEM
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyCors",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:3000")
//                   .AllowAnyMethod()
//                   .AllowAnyHeader()
//                   .AllowCredentials(); // Cho phép thông tin xác thực
//        });
//});
//builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
//{
//    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
//}));

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyCors", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddScoped<ExcelServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IQrCodeServices, QrCodeServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*app.UseRouting(); */// thêm, k cần vẫn chạy đc
app.UseCors("MyCors"); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<HubSignalR>("/Hub-SignalR"); // dùng cái trên này cũng đc

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapHub<HubSignalR>("/Hub-SignalR");
//    // Các endpoints khác
//});
app.Run();


// note: signalr: 
//+ thay đổi ở bên controller



