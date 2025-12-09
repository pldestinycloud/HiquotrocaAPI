using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Presentation
{
    public static class LookUpEntitiesEndpoints
    {
        public static void MapLookUpEntitiesEndpoints(this WebApplication app)
        {

            app.MapGet("/Actiontypes", async (AppDbContext db) =>
            {
                var items = await db.ActionTypes
                    .Where(a => !a.IsDeleted)
                    .AsNoTracking()
                    .Select(a => new { a.Id, a.Name, a.Description })
                    .ToListAsync();

                return Results.Ok(items);

            })
            .RequireCors("OpenPolicy");
            //.RequireAuthorization();

            app.MapGet("/Categories", async (AppDbContext db) =>
            {
                var items = await db.Categories
                    .Where(c => !c.IsDeleted)
                    .AsNoTracking()
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.IconPath,
                        SubCategories = c.SubCategories.Select(sc => new
                        {
                            sc.Id,
                            sc.Name,
                            sc.CategoryId
                        })
                    })
                    .ToListAsync();

                return Results.Ok(items);
            })
            .RequireCors("OpenPolicy");
            //.RequireAuthorization();

            app.MapGet("/Categories/{id}", async (long id, AppDbContext db) =>
            {
                var item = await db.Categories
                    .Where(c => c.Id == id && !c.IsDeleted)
                    .AsNoTracking()
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.IconPath,
                        SubCategories = c.SubCategories.Select(sc => new { sc.Id, sc.Name, sc.CategoryId })
                    })
                    .FirstOrDefaultAsync();

                return item is null ? Results.NotFound() : Results.Ok(item);
            })
            .RequireCors("OpenPolicy");
            //.RequireAuthorization();

            app.MapGet("/Countries", async (AppDbContext db) =>
            {
                var items = await db.Countries
                    .Where(c => !c.IsDeleted)
                    .AsNoTracking()
                    .Select(c => new { c.Id, c.Name, c.IsoCode })
                    .ToListAsync();

                return Results.Ok(items);
            })
            .RequireCors("OpenPolicy");
            //.RequireAuthorization();
        }
    }
}