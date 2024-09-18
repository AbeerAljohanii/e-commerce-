using e_commerce_;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//category Apis
List<Category> categories = new List<Category>()
{
    new Category("Painting"),
    new Category("Sculpture"),
};

//get categories
app.MapGet(
    "/api/v1/categories",
    () =>
    {
        if (categories.Count == 0)
        {
            return Results.NotFound("list is empty");
        }
        return Results.Ok(categories);
    }
);

//create category
app.MapPost(
    "/api/v1/categories",
    (Category category) =>
    {
        category.Id = Guid.NewGuid();
        categories.Add(category);
        return Results.Created("Category has added successfully", category);
    }
);

//update category
app.MapPut(
    "/api/v1/categories/{id}",
    (Category category, Guid id) =>
    {
        Category foundCategory = categories.FirstOrDefault(c => c.Id == id);
        if (foundCategory == null)
        {
            return Results.NotFound("Category not found in the list");
        }
        foundCategory.Name = category.Name;
        return Results.Ok(category);
    }
);

//delete category
app.MapDelete(
    "/api/v1/categories/{id}",
    (Guid id) =>
    {
        Category foundCategory = categories.FirstOrDefault(c => c.Id == id);
        if (foundCategory == null)
        {
            return Results.NotFound("Category not found in the list");
        }
        categories.Remove(foundCategory);
        return Results.NoContent();
    }
);

app.Run();
