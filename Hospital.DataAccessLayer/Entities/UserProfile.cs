namespace Hospital.DataAccessLayer.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
