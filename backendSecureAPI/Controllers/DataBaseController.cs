using DAL;

namespace Controllers
{
    public class DataBaseController
    {
        private DataAccesLawyer _dal = new DataAccesLawyer();

        public bool IsDatabaseConnected() { return _dal.IsDatabaseConnected(); }
        public bool UserExist(string username, string password) {return _dal.UserExist(username); }

        /// <summary>
        /// Register a new user if not exists
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool RegisterUser(string username, string password)
        {
            if (!UserExist(username, password))
            {
                bool isRegistered = _dal.RegisterUser(username, password);
                return isRegistered;
            }
            else
            {
                return false;
            }
        }
    }
}