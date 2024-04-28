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
app.MapControllerRoute(
    name: "list-user",
    pattern: "list-user",
    defaults: new { Controller = "User", action = "Index" }
    );
app.MapControllerRoute(
    name: "create-user",
    pattern: "create-user",
    defaults: new { Controller = "User", action = "Create" }
    );
app.MapControllerRoute(
    name: "edit-user",
    pattern: "edit-user/{id?}",
    defaults: new { Controller = "User", action = "Edit" }
    );
app.MapControllerRoute(
    name: "delete-user",
    pattern: "delete-user/{id?}",
    defaults: new { Controller = "User", action = "Delete" }
    );
app.MapControllerRoute(
    name: "list-order",
    pattern: "list-order",
    defaults: new { Controller = "Order", action = "Index" }
    );
app.MapControllerRoute(
    name: "chitiet-donhang",
    pattern: "chitiet-donhang/{id?}",
    defaults: new { Controller = "Order", action = "Details" }
    );
app.MapControllerRoute(
    name: "xoa-donhang",
    pattern: "xoa-donhang/{id?}",
    defaults: new { Controller = "Order", action = "Delete" }
    );
app.Run();
