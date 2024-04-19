public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalRepository.CreateAnimal(animal);
    }

    public int DeleteAnimal(int idAnimal)
    {
        return _animalRepository.DeleteAnimal(idAnimal);
    }

    public IEnumerable<Animal> GetAnimals(string param)
    {
        return _animalRepository.GetAnimals(param);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalRepository.UpdateAnimal(animal);
    }
    public IEnumerable<Animal> PrintAnimals()
    {
        return _animalRepository.PrintAnimals();
    }
}