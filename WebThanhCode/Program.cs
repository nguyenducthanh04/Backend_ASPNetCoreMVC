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
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");
app.MapControllerRoute(
    name: "trang-chu",
    pattern: "trang-chu",
    defaults: new { Controller = "Product", action = "Index" }
    );
app.MapControllerRoute(
    name: "trang-chu",
    pattern: "them-moi-product",
    defaults: new { Controller = "Product", action = "Create" }
    );
app.MapControllerRoute(
    name: "detail",
    pattern: "detail/{id?}",
    defaults: new { Controller = "Product", action = "Details" }
    );
app.MapControllerRoute(
    name: "edit",
    pattern: "edit-product/{id?}",
    defaults: new { Controller = "Product", action = "Edit" }
    );
app.MapControllerRoute(
    name: "add-image",
    pattern: "add-image/{id?}",
    defaults: new { Controller = "Product", action = "AddImage" }
    );
app.MapControllerRoute(
    name: "edit",
    pattern: "edit-image/{id?}",
    defaults: new { Controller = "Product", action = "EditImage" }
    );
app.MapControllerRoute(
    name: "add",
    pattern: "add-image-related/{id?}",
    defaults: new { Controller = "Product", action = "AddRelatedPhoto" }
    );
app.MapControllerRoute(
    name: "delete",
    pattern: "delete-prd/{id?}",
    defaults: new { Controller = "Product", action = "Delete" }
    );
app.Run();
