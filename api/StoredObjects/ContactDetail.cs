namespace StoredObjects
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string Value { get; set; }
        public string Auth0UserId { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public bool Verified { get; set; }
    }
}