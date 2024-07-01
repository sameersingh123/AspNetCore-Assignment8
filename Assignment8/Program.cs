var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

Dictionary<int,string> countries=new Dictionary<int,string>();
countries.Add(1,"United States");
countries.Add(2,"Canada");
countries.Add(3,"United Kingdom");
countries.Add(4,"India");
countries.Add(5,"Japan");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries", async context =>
    {
        foreach(var country in countries)
        {
            await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
        }
    });

    endpoints.MapGet("/countries/{countryID:int:range(1,100)}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("countryID") == false)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The CountryID should be between 1 and 100");
        }

        int countryID = Convert.ToInt32(context.Request.RouteValues["countryID"]);
        if (countries.ContainsKey(countryID))
        {
            await context.Response.WriteAsync($"{countries[countryID]}");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("No Country");
        }

    });

});

app.Run(async context =>
{
    await context.Response.WriteAsync("No Response");
});

app.Run();
