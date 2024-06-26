import React, { useState, useEffect }  from 'react';
// import { AuthContext } from '../context/AuthProvider';

// USE CASE #1: As a registered user, I want to log in
// securely so that I cannot be held responsible for 
// someone else’s actions, so I can ensure the security of my account.
export const LoginForm = () => {
    //declare variables for form fields
    // const {authUser, setAuthUser} = useContext(AuthContext);
    const [username, setUsername] = useState('');
    const [enterPasswordMode, setEnterPasswordMode] = useState(false);
    const [password, setPassword] = useState(''); 
    const [showPassword, setShowPassword] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const url = 'http://localhost:5206/Auth/startLogin';
        //Implement submitting form data to the server
        console.log('in Login form, handleSubmit');
        try {
            console.log('in try block')
            if (validateForm()) {
                console.log('Form is valid. ');
                
                const response = await fetch (url, {
                    method: 'POST',
                    credentials: 'include',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(username)
                });
                if (response.ok) {
                    const data = await response.json()
                    setEnterPasswordMode(true);
                    alert('Username valid. Please enter OTP sent to your email.');
                    console.log("in enter password mode:", {enterPasswordMode})
                    // const isLoggedIn = data.isLoggedIn
                    // if (isLoggedIn) {
                    //     setAuthUser(data);
                    //     navigate(`/viewpolls`, {replace:true});
                    // }
                } else {
                    alert('Login failed. Please try again.');
                }
            } else {
                alert('Fields are not valid. Please try again.');
            }
        } catch (error) {
            console.log('Error in login: ', error);
        }
    }

    const validateForm = () => {
        if (username === '') {
            alert('Please fill out all fields');
            return false;
        }
        return true;
    }

  return (
    <div className="font-sans min-w-[500px] min-h[586px] bg-light-grey-1 rounded-2xl">
    <div className="flex flex-col items-center pt-28 px-12">
      <h3 className='text-3xl font-semibold pb-1'>Login</h3>
      <form className='justify-start py-10' onSubmit={handleSubmit}> {/* form*/}
        <div className="pb-2"> {/* field*/}
            <input type="text"
            name='username'
            placeholder="Username"
            onChange={(e) => setUsername(e.target.value)}
            className="field-input"
            />
        </div>
        {enterPasswordMode && (
            <div className='pb-2'>
                <input 
                    type={
                        showPassword ? "text" : "password"
                    }
                    name='password'
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="field-input"
                />
                <input 
                    type="checkbox" 
                    id="check" 
                    onClick={() => setShowPassword((prev) => !prev)} />
                <label className='px-2' htmlFor="check">Show Password</label>
            </div>
            )
        }
        <div className='flex flex-col items-center justify-center m-2 overflow-auto py-4'>
            <button type="submit" onClick={() => {validateForm()}}
                className="btn-register">
                {enterPasswordMode ? "Login" : "Enter username"}
            </button>
            <a className='text-sm py-4 italic hover:underline' href='/'>Don't have an account? Register</a>
        </div>
      </form>
    </div>
  </div>
  )
}

export default LoginForm