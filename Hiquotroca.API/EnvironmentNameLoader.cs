namespace Hiquotroca.API;

public class EnvironmentNameLoader
{
    public static string GetEnvironmentName()
    {
        // Load environment name from Environment.md file (Should have values: Development, Production)
        // or default to Development
        // It should be loaded from environment variables, but since we don't have access to them in DinaHosting,
        // we are using this workaround

        //Sets default environment as Development
        //Also it serves avoids the creation of a MD file in local development machines
        var environmentName = "Development";

        var markerFileName = "Environment.md";
        var markerPath = Path.Combine(Directory.GetCurrentDirectory(), markerFileName);

        if (File.Exists(markerPath))
        {
            try
            {
                var content = File.ReadAllText(markerPath).Trim();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    environmentName = content;
                }
            }
            catch
            {
            }
        }

        return environmentName;
    }
}
