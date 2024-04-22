public class Animal(string name, string description, string category, string area)
{
    public int IdAnimal { get; set; }
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string Category { get; set; } = category;
    public string Area { get; set; } = area;

}