using LocalServices;
using DAL;
using System.Data;

namespace Controllers
{
    public class DataBaseController
    {
        private DataAccesLawyer _dal = new DataAccesLawyer();

        public bool IsDatabaseConnected() { return _dal.IsDatabaseConnected(); }
        public bool UserExist(string username) {return _dal.UserExist(username); }

        /// <summary>
        /// Register a new user if not exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool RegisterUser(string username)
        {
            if (!UserExist(username))
            {
                RandomGenerator rdnKey = new RandomGenerator();
                Encryptator encrypt = new Encryptator();
                bool isRegistered = _dal.RegisterUser(username, encrypt.EncriptarAES(rdnKey.CreateRandomString(),username));
                return isRegistered;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetAllUserKeys()
        {
            Encryptator encrypt = new Encryptator();
            DataTable dt = _dal.GetAllEncryptedUserKeys();
            List<string> listUnencryptKeys = dt.AsEnumerable()
                                               .Select(row => encrypt.DesencriptarAES(row.Field<string>("_Key"),row.Field<string>("_Name")))
                                               .ToList();
            return listUnencryptKeys;
        }
    }
}