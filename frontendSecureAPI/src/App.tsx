import './App.css'

function App() {

  return (
    <>
      <h1>Registration Form</h1>
      <img src="src/assets/user_icon.png" alt="user" />
      <form>
        <input type="text" name="username" id="username" />
        <input type="password" name="userpassword" id="userpassword" />
        <input type="button" value="Register" />
      </form>
    </>
  );
}

export default App