using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Helpers;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPatientMastersService, PatientMastersService>();
builder.Services.AddScoped<IForgetPasswordService, ForgetPasswordService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBDataLoaderPortalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
