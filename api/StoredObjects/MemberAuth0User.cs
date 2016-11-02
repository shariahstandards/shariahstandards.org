namespace StoredObjects
{
    public class MemberAuth0User
    {
        public int Id { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public string Auth0UserId { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public bool Confirmed { get; set; }
    }
}