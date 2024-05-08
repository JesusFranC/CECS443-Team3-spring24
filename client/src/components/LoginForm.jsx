import React, { useState, useEffect }  from 'react'

// USE CASE #1: As a registered user, I want to log in
// securely so that I cannot be held responsible for 
// someone else’s actions, so I can ensure the security of my account.
export const LoginForm = () => {
    //declare variables for form fields
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState(''); 
    const [showPassword, setShowPassword] = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault();
        //Implement submitting form data to the server
        console.log('submitted: ', username, password);
    }

    const validateForm = () => {
        if (username === '' || password === '') {
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
        </div>
        <input 
            type="checkbox" 
            id="check" 
            onClick={() => setShowPassword((prev) => !prev)} />
        <label className='px-2' for="check">Show Password</label>
        
        <div className='flex flex-col items-center justify-center m-2 overflow-auto py-4'>
            <button type="submit" onClick={() => {validateForm()}}
                className="btn-register">
                Login
            </button>
            <a className='text-sm py-4 italic hover:underline' href='/'>Don't have an account? Register</a>
        </div>
        
      </form>
    </div>
  </div>
  )
}

export default LoginForm