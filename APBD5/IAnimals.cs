using APBD5.Model;

namespace APBD5;

public interface IAnimals
{
    List<Animal> FindAll(string orderBy);
    void Add(Animal animal);
    bool Update(int id, Animal animal);
    bool Delete(int id);

}