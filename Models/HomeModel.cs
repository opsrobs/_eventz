namespace eventz.Models;
public class Home
{
    public UserModel? User { get; set; }
    public List<Category> Categories { get; set; }
    public List<Section> Sections { get; set; }
}
