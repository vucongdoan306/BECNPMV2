
using MISA.BLL.Interfaces;
using MISA.BLL.Services;
using MISA.Common.Entities;
using MISA.DAL.BaseRepository;
using MISA.DAL.Interface;
using MISA.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IBaseService<Employee>, BaseBL<Employee>>();
builder.Services.AddScoped<IBaseService<Department>, BaseBL<Department>>();
builder.Services.AddScoped<IBaseRepository<Employee>, BaseRepository<Employee>>();
builder.Services.AddScoped<IBaseRepository<Department>, BaseRepository<Department>>();
builder.Services.AddScoped<IEmployeeService, EmployeeBL>();
builder.Services.AddScoped<IDepartmentService, DepartmentBL>();
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    {
        b.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().Build();
    }
    );
}
);

builder.Services.AddControllers().AddNewtonsoftJson(

);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
