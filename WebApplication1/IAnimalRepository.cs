public interface IAnimalRepository
{
    IEnumerable<Animal> PrintAnimals();
    IEnumerable<Animal> GetAnimals(string param);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
    
}