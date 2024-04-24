var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
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
app.UseSession();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "home",
    pattern: "home",
    defaults: new { Controller = "Home", action = "Index" }
    );
app.MapControllerRoute(
    name: "product",
    pattern: "products",
    defaults: new { Controller = "Product", action = "Index" }
    );
app.MapControllerRoute(
    name: "product-detail",
    pattern: "product-detail/{id?}",
    defaults: new { Controller = "Product", action = "Details" }
    );
app.MapControllerRoute(
    name: "get-products",
    pattern: "get-products",
    defaults: new { Controller = "Product", action = "GetProductsByColor" }
    );
app.MapControllerRoute(
    name: "account",
    pattern: "account",
    defaults: new { Controller = "Access", action = "Account" }
    );
app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { Controller = "Access", action = "Login" }
    );
app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { Controller = "Access", action = "Logout" }
    );
app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { Controller = "Access", action = "Register" }
    );
app.Run();
