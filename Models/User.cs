using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }         
        
        public string Bio { get; set; }  

        public AppFile Image { get; set; }       
    }
}