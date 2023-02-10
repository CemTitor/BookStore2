using BookStore2.DbOperations;

public static class Extensions
{
    public static void InitializeDatabase(this IHost host)
    {
        {
    
            using (var scope = host.Services.CreateScope())
            {
                /// We are getting the service provider from the scope
                var services = scope.ServiceProvider;
                /// In initialize method, we are creating the database
                DataGenerator.Initialize(services);
            }
        }
    }
}