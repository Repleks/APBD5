public static class AnimalConfiguration
{
    public static IEndpointRouteBuilder RegisterAnimalEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/animals", (IAnimalService service) => TypedResults.Ok(service.PrintAnimals()));
        endpoints.MapGet("/api/animals/{orderBy?}", (string orderBy, IAnimalService service) => TypedResults.Ok(service.GetAnimals(orderBy ?? "name")));
        endpoints.MapPost("/animals", (Animal animal, IAnimalService service) => TypedResults.Created("", service.CreateAnimal(animal)));
        endpoints.MapPut("/animals/{idAnimal:int}", (int idAnimal, Animal animal, IAnimalService service) =>
        {
            animal.IdAnimal = idAnimal;
            service.UpdateAnimal(animal);
            return TypedResults.NoContent();
        });
        endpoints.MapDelete("/animals/{idAnimal:int}", (int idAnimal, IAnimalService service) =>
        {
            service.DeleteAnimal(idAnimal);
            return TypedResults.NoContent();
        });
        return endpoints;
    }
}