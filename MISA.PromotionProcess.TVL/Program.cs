using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MISA.PromontionProcess.BL;
using MISA.PromontionProcess.BL.JwtToken;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.BL.EmployeeBL;
using MISA.PromotionProcess.BL.RequestBL;
using MISA.PromotionProcess.BL.RequestMemberBL;
using MISA.PromotionProcess.BL.StructureBL;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.EmployeeDL;
using MISA.PromotionProcess.DL.ProductDL;
using MISA.PromotionProcess.DL.RequestDL;
using MISA.PromotionProcess.DL.RequestMemberDL;
using MISA.PromotionProcess.DL.StructureDL;
using MISA.PromotionProcess.DL.UserDL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IProductDL, ProductDL>();
builder.Services.AddScoped<IProductBL, ProductBL>();

builder.Services.AddScoped<IStructureDL, StructureDL>();
builder.Services.AddScoped<IStructureBL, StructureBL>();

builder.Services.AddScoped<IRequestDL, RequestDL>();
builder.Services.AddScoped<IRequestBL, RequestBL>();
    
builder.Services.AddScoped<IRequestMemberDL, RequestMemberDL>();
builder.Services.AddScoped<IRequestMemberBL, RequestMemberBL>();
    
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();

builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));


// add authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var keyBytes = Encoding.ASCII.GetBytes(key);
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateAudience = false,
    };
});

//builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();

var app = builder.Build();

//Chuỗi kết nối db
DBContext.ConnectionStrings = builder.Configuration.GetConnectionString("MyConnection");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
