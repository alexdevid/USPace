namespace Model.Space
{
    public abstract class Object
    {
        readonly int Id;
        readonly string Name;

        Object(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}