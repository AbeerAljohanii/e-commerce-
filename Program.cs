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

List<Customer> customers = new List<Customer>
{
    new Customer
    {
        Id = 1,
        Name = "Manar",
        PhoneNumber = "0505050505",
        Email = "manar@gmail.com",
        Password = "mama",
    },
    new Customer
    {
        Id = 2,
        Name = "Abeer",
        PhoneNumber = "0505050555",
        Email = "abeer@gmail.com",
        Password = "abab",
    },
};

// Define the GET endpoint to return the list of customers
app.MapGet(
    "/api/v1/customers",
    () =>
    {
        return Results.Ok(customers);
    }
);

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
            return Results.NotFound("List is empty");
        }
        return Results.Ok(categories);
    }
);

//create category
app.MapPost(
    "/api/v1/categories",
    (Category category) =>
    {
        categories.Add(new Category(category.Name));
        return Results.Created("Category has been added successfully", category);
    }
);

//update category
app.MapPatch(
    "/api/v1/categories/{id}",
    (Category category, Guid id) =>
    {
        Category foundCategory = categories.FirstOrDefault(c => c.Id == id);
        if (foundCategory == null)
        {
            return Results.NotFound("Category not found");
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
            return Results.NotFound("Category not found");
        }
        categories.Remove(foundCategory);
        return Results.NoContent();
    }
);

app.Run();
