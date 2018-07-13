using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  //public class UserWitnessInvitation
  //{
  //  public int Id { get; set; }
  //  public string UserId { get; set; }
  //  public virtual Auth0User Auth0User { get; set; }
  //  public string Email{ get; set; }
  //}
  public class UserPhotograph
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public byte[] Value { get; set; }
    public string MimeType { get; set; }
  }
  public class UserEmail
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public string Value { get; set; }
    public bool Verified { get; set; }
  }
  public class UserMobilePhone
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public string Value { get; set; }
    public bool Verified { get; set; }
  }
  public class UserFirstName
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public string Value { get; set; }
  }
  public class UserFathersFirstName
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public string Value { get; set; }
    public int AgeSequence { get; set; }
  }
  public class UserBirthLocation
  {
    public string UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }
    public double Longitude{ get; set; }
    public double Latitude {get; set; }
    public string Town { get; set; }
    public string Country { get; set; }
  }
}
