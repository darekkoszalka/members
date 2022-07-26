using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MembersBlazorNet6.Areas.Identity;
using MembersBlazorNet6.Data;
using MembersBlazorNet6.StaticData;
using Radzen;
using System.Security.Claims;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.Services.AccessServices;
using MembersBlazorNet6.Data.Repositories.IRepository;
using MembersBlazorNet6.Data.Repositories.Repository;
using MembersBlazorNet6.Services.ExportToExcel;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


//builder.Services.AddSignalR().AddAzureSignalR(options =>
//{
//    options.ServerStickyMode = Microsoft.Azure.SignalR.ServerStickyMode.Required;
//});


builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RoleSD.SuperAdmin, policy => policy.RequireClaim(RoleSD.SuperAdmin));
    options.AddPolicy(RoleSD.UserAdmin, policy => policy.RequireClaim(RoleSD.UserAdmin));
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

builder.Services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
{
    var provieder = (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>();
    return provieder;
}
);
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IUnionInstitutionRepository, UnionInstitutionRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IUnionStructureRepository, UnionStructureRepository>();
builder.Services.AddScoped<IMemberPaymentRepository, MemberPaymentRepository>();
builder.Services.AddScoped<IExcelService, ExcelService>();

var serviceProvider = builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

DbInitializer.InitialMigrations(context);
DbInitializer.AddTypeOfUnionInstitution(context);
DbInitializer.AddMemberWorkPlaces(context);
DbInitializer.AddMemberEducation(context);
DbInitializer.AddUnionFuction(context);
DbInitializer.AddUnionStuctureType(context);
DbInitializer.AddZG(context);

CreateUserRoles(serviceProvider).Wait();

app.Run();

async Task CreateUserRoles(IServiceProvider serviceProvider)
{
    //initializing custom roles
    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string[] roleNames = { RoleSD.SuperAdmin, RoleSD.Admin, RoleSD.User, RoleSD.UserAdmin };
    IdentityResult roleResult;
    foreach (var roleName in roleNames)
    {
        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            //create the roles and seed them to the database
            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
    ApplicationUser user = await UserManager.FindByEmailAsync("admin@admin.com");
    if (user == null)
    {
        user = new ApplicationUser()
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            FirstName = "Name",
            LastName = "LastName",
            RegisterDate = DateTime.Now,
            EmailConfirmed = true,
            PhoneNumber = "PhoneNumber"
        };
        await UserManager.CreateAsync(user, "Pasword1!");
    }
    await UserManager.AddToRoleAsync(user, RoleSD.SuperAdmin);
    var userClaims = await UserManager.GetClaimsAsync(user);
    if (userClaims == null || userClaims.Count == 0)
    {
        var claim = new Claim(RoleSD.SuperAdmin, RoleSD.SuperAdmin);
        await UserManager.AddClaimAsync(user, claim);
    };
    var userAccesses = await context.UserAccess
        .Where(u => u.UserId == user.Id)
        .ToListAsync();
    if (userAccesses == null || userAccesses.Count == 0)
    {
        var access = new UserAccess()
        {
            UserId = user.Id,
            Access = RoleSD.SuperAdmin,
            Lock = false
        };
        context.UserAccess.Add(access);
        await context.SaveChangesAsync();
    }
}