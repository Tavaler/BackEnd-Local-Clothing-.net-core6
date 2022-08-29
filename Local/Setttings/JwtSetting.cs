namespace  Local.Settings
{
    public class JwtSetting
    {
        public string Key { get; set; } = "SecretKey@12345678";
        public string Issuer { get; set; } = "MrTeeradet";
        public string Audience { get; set; } = "Student Comscience";
        public string Expire { get; set; } = "1";
    }

}
