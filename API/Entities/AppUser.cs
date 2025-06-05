namespace App.Entities;

public class AppUser
{
   // public int Myproperty()
   public int Id { get; set; }

   public required string Username { get; set; }
   public required byte[] PasswordHash { get; set; }
   public required byte[] PasswordSalt { get; set; }
}