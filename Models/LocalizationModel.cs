namespace eventz.Models
{
    public class Localization
    {
        public Guid Id { get; set; }
        public Guid? UserID { get; set; }
        public UserModel? User { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Localization(Guid id, Guid userID, UserModel? user, double longitude, double latitude)
        {
            Id = id;
            UserID = userID;
            User = user;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Localization(Guid id, double longitude, double latitude)
        {
            Id = id;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Localization(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }


}
