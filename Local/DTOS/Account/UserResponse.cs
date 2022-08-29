using Local.Models;

namespace Local.DTOS.Account
{
    public class UserResponse
    {
        public string UserId { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        //public string UserProfile { get; set; } = "";
        public string UserPhone { get; set; }

        public string UserIsuse { get; set; } = "1";
        public DateTime UserCreate { get; set; } = DateTime.Now;


        public string RoleName { get; set; }

        public virtual Role Role { get; set; }

        static public UserResponse FromUser(User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                UserFirstname = user.UserFirstname,
                UserCreate = user.UserCreate,
                UserEmail = user.UserEmail,
                UserLastname = user.UserLastname,
                UserPhone = user.UserPhone,
                UserIsuse = user.UserIsuse,
                UserPassword = user.UserPassword,

                //UserProfile =user.UserProfile,
                

            };
        }
        //[ForeignKey("RoleId")]
        

        static public object User(User data) => new
        {
            data.UserId,
            data.UserEmail,
            data.UserFirstname,
            //ProfileImage = !string.IsNullOrEmpty(data.ProfileImage) ? Constants.PathServer + data.ProfileImage : null,
            data.UserCreate,
            data.RoleId,
        };
    }
}
