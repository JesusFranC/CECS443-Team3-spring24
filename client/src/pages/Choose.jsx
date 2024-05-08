import React from 'react'

export const Choose= () => {
  return (
    <div>
      <div className='flex items-start '> 
      {/* left container */}
        <div className='flex-auto flex flex-col justify-center items-center w-1/2 h-screen '>
        <button type="submit" className="btn-select text-white">
            Polls
          </button>
        </div>
        {/* right container */}
        <div className='flex-auto flex flex-col justify-center items-center w-1/2 h-screen'>
        <button type="submit" className="btn-select text-white">
            Ratings
          </button>
        </div>
      </div>
      
    </div>
    

  )
}

export default Choose