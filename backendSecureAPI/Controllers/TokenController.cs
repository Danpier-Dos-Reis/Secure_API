using LocalServices;

namespace Controllers{
    //This class create and delete tokens
    public class TokenController{

        RandomGenerator _randomGenerator = new RandomGenerator();
        TokenMaker _tokenMaker = new TokenMaker();

        /// <summary>
        /// Make a token for the user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>string TOKEN</returns>
        public string CreateToken(string username, string userKey){
            DataBaseController dbController = new DataBaseController();
            
            // Check if the database is connected
            if (dbController.IsDatabaseConnected())
            {
                // Check if the user exists in the database
                if (dbController.UserExist(username))
                {
                    return _tokenMaker.MakeToken(_randomGenerator.CreateRandomString(),username,userKey);
                }
                else
                {
                    return "User does not exist.";
                }
            }
            else
            {
                return "Database connection failed.";
            }
        }
    }
}