
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> PrintAnimals()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Animals";
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        while (reader.Read())
        {
            animals.Add(new Animal
            {
                IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                Area = reader.GetString(reader.GetOrdinal("Area"))
            });
        }
        return animals;
    }
    // public IEnumerable<Animal> GetAnimals(string param)
    // {
    //     using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    //     connection.Open();
    //     var command = connection.CreateCommand();
    //     command.CommandText = "SELECT * FROM Animals order by @param";
    //     command.Parameters.AddWithValue("@param", param);
    //     using var reader = command.ExecuteReader();
    //     var animals = new List<Animal>();
    //     while (reader.Read())
    //     {
    //         animals.Add(new Animal
    //         {
    //             IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
    //             Name = reader.GetString(reader.GetOrdinal("Name")),
    //             Description = reader.GetString(reader.GetOrdinal("Description")),
    //             Category = reader.GetString(reader.GetOrdinal("Category")),
    //             Area = reader.GetString(reader.GetOrdinal("Area"))
    //         });
    //     }
    //     return animals;
    // }
    public IEnumerable<Animal> GetAnimals(string param)
    {
        var validColumns = new List<string> { "IdAnimal", "Name", "Description", "Category", "Area" };
        if (!validColumns.Contains(param))
        {
            throw new ArgumentException("Invalid column name", nameof(param));
        }

        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Animals ORDER BY {param}";
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        while (reader.Read())
        {
            animals.Add(new Animal
            {
                IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Category = reader.GetString(reader.GetOrdinal("Category")),
                Area = reader.GetString(reader.GetOrdinal("Area"))
            });
        }
        return animals;
    }
    public int CreateAnimal(Animal animal)
    {
        var validationContext = new ValidationContext(animal);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(animal, validationContext, validationResults, true))
        {
            throw new ArgumentException("Invalid animal data", nameof(animal));
        }

        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Animals (IdAnimal, Name, Description, Category, Area) VALUES (@IdAnimal, @Name, @Description, @Category, @Area)";
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);
        return command.ExecuteNonQuery();
    }
    public int UpdateAnimal(Animal animal)
    {
        var validationContext = new ValidationContext(animal);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(animal, validationContext, validationResults, true))
        {
            throw new ArgumentException("Invalid animal data", nameof(animal));
        }

        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Animals SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);
        return command.ExecuteNonQuery();
    }
    public int DeleteAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Animals WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", idAnimal);
        return command.ExecuteNonQuery();
    }
}