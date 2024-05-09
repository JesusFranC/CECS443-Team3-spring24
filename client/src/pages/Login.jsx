import React from 'react'
import LoginForm from '../components/LoginForm'
import lblogo from '../assets/lblogo.png'

export const Login = () => {
  return (
    <div>
      <div className='flex items-start mb-4 p-8'> 
      {/* left container */}
        <div className='flex-auto flex flex-col justify-center items-center pt-32 w-1/2 h-sceen m-1 p-4'>
            <img src={lblogo} alt='LB State Logo' className='w-1/4 h-1/4 m-10'/>
            <h1 className='text-3xl font-semibold pb-4 text-black'>Rating and Polling</h1>
            <p className='text-lg  text-black'>California State University, Long Beach</p>
            <p className='text-md  text-black italic'>For active CSULB students, faculty only*</p>
        </div>
        {/* right container */}
        <div className='flex-auto flex flex-col justify-center items-center w-1/2 h-screen'>
          <LoginForm/>
        </div>
      </div>
    </div>
  )
}

export default Login