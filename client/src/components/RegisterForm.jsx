import React, { useState, useEffect }  from 'react'


//USE CASE #1: I want to be able to As a registered user, I want to log in
// securely so that I cannot be held responsible for someone else’s actions, so I can ensure the security of my account.
export const RegisterForm = () => {
    //declare variables for form fields
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');   
    
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('submitted: ', username, password);
    }

    const validateForm = () => {
        if (username === '' || password === '') {
            alert('Please fill out all fields');
        }
    }
  return (
    <div className="font-sans w-[520px] h-[586px] bg-light-grey-1 rounded-2xl">
    <div className="flex flex-col pt-20 px-12">
      <h3 className='text-3xl font-semibold pb-4'>Register for Rating and Polling</h3>
      <form className='justify-start py-3' onSubmit={handleSubmit}> {/* form*/}
        <div className="pb-2"> {/* field*/}
            <input type="text"
            name='username'
            placeholder="Enter your username"
            onChange={(e) => setUsername(e.target.value)}
            className="flex items-center m-auto w-[310px] h-[50px] bg-[#eaeaea] rounded-md p-[14px] focus:outline-none"
            />
        </div>
        <div className='pb-2'>
            <input type="text"
                name='password'
                placeholder="Enter your password"
                onChange={(e) => setPassword(e.target.value)}
                className="flex items-center m-auto w-[310px] h-[50px] bg-[#eaeaea] rounded-md p-[14px] focus:outline-none"
            />
        </div>
        <div className='pb-2'>
            <input type="text"
                name='password'
                placeholder="Enter your password again"
                onChange={(e) => setPassword(e.target.value)}
                className="flex items-center m-auto w-[310px] h-[50px] bg-[#eaeaea] rounded-md p-[14px] focus:outline-none"
            />
        </div>
        <div className='flex flex-col items-center justify-center m-2 overflow-auto'>
            <button type="submit" onClick={() => {validateForm()}}
                className="btn-register">
                Register
            </button>
            <a className='text-sm py-4' href='/'>Already have an account? Login</a>
        </div>
        
      </form>
      
      <div className='w-16 h-1.5 mt-3 bg-lbsu-blue rounded-lg'></div>
    </div>
  </div>
  )
}

export default RegisterForm