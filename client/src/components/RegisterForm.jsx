import React, { useState, useEffect }  from 'react'

//USE CASE #1: As a registered user, I want to log in
// securely so that I cannot be held responsible for 
// someone else’s actions, so I can ensure the security of my account.
export const RegisterForm = () => {
    //declare variables for form fields
    const [email, setEmail] = useState(''); 
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');   
    const [password2, setPassword2] = useState('');
    const [showPassword, setShowPassword] = useState(false);

    
    const handleSubmit = async (e) => {
        e.preventDefault();
        //Implement submitting form data to the server
        const url = 'http://localhost:5206/Registeration/Register';
        try {
            const response = await fetch (url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(email) //FIXME: add username, password
            });
            if (response.ok) {
                console.log('User registered', {email});
                e.target.reset(); 
            } else {
                alert('Registration failed. Please try again.');
            }
        } catch (error) {
            console.log('Error in registration: ', error);
        }
        console.log('submitted: ', email);
    }

  return (
    <div className="font-sans min-w-[500px] min-h[586px] bg-light-grey-1 rounded-2xl">
    <div className="flex flex-col items-center pt-28 px-12">
      <h3 className='text-3xl font-semibold pb-4'>Register for Rating and Polling</h3>
      <form className='justify-start py-10' onSubmit={handleSubmit}> {/* form*/}
        <div className="pb-2"> {/* field*/}
            <input type="text"
            name='username'
            placeholder="Enter your email"
            onChange={(e) => setEmail(e.target.value)}
            className="field-input"
            />
        </div>
        
        <div className='flex flex-col items-center justify-center m-2 overflow-auto py-4'>
            <button type="submit"
                className="btn-register">
                Register
            </button>
            <a className='text-sm py-4 italic hover:underline' href='/login'>Already have an account? Login</a>
        </div>
        
      </form>
    </div>
  </div>
  )
}

export default RegisterForm