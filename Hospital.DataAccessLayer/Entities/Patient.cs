namespace Hospital.DataAccessLayer.Entities
{
    public class Patient
    {

        public int Id { get; set; }
        public bool IsDischarged { set; get; }
        public bool IsConfirmed { set; get; }

        public virtual UserProfile Profile { get; set; }
        public virtual MedicalCard MedicalCard { set; get; }
        public virtual Doctor Doctor { set; get; }
    }
}
