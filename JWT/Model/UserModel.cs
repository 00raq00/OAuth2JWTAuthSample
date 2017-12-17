using System;

namespace JWT.Model
{
  public class UserModel
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Website { get; internal set; }
  }
}
