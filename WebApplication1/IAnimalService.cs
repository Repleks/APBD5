public interface IAnimalService
{
    IEnumerable<Animal> GetAnimals(string param);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
    IEnumerable<Animal> PrintAnimals();
}