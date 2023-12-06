namespace eventz.Models;
public class Home
{
    public Guid Id { get; set; }
    public UserModel? User { get; set; }
    public List<Category> Categories { get; set; }
    public List<Section> Sections { get; set; }

    public Home(Guid id, UserModel? user, List<Category> categories, List<Section> sections)
    {
        Id = id;
        User = user;
        Categories = categories;
        Sections = sections;
    }

    public Home()
    {
    }
}
