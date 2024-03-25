using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace gameForMood.Services.Admin.AdminServices.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "gameForMoodApiServer";
        public const string AUDIENCE = "gameForMoodApiClient";
        const string KEY = "qJEuiR7u87yamaj7211910_!214karosf32+_12";
        public const int LIFETIME = 3;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        public const string SecretKey = "RUlkm8d3KSPFE3jL5bk93crJpbKtYchE";
    }
}
