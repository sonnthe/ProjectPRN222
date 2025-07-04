using ProjectPRN222.DataAccessLayers.Impl;
using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Services.Impl;
using ProjectPRN222.Services.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ISubjectService, SubjectService>();
builder.Services.AddSingleton<ISubjectDA, SubjectDAO>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IAccountDA, AccountDAO>();
builder.Services.AddSingleton<ILessonService, LessonService>();
builder.Services.AddSingleton<ILessonDA, LessonDAO>();
builder.Services.AddSingleton<IPackageService, PackageService>();
builder.Services.AddSingleton<IPackageDA, PackageDAO>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICategoryDA, CategoryDAO>();
builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<IRegistrationDA, RegistrationDAO>();

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}"
);
app.UseSession();

app.Run();
