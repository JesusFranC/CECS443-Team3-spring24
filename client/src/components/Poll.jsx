import React, { useState, useEffect }  from 'react'

//USE CASE #4: I want to view reviews and ratings for different campus entities, so
//I can make informed decisions about my campus experiences.
export const Poll = ({ title, description, maxVotes, pollOptions = [] }) => {
  title = 'What is your favorite food?'
  description = 'We are trying to decide what to serve at the next event.'
  maxVotes = 4

  pollOptions = ['Pizza', 'Tacos', 'Burgers', 'Salad']
  return (
    <div className="font-sans flex justify-start bg-lbsu-yellow max-w-[240px] max-h-[135px]">
      <h3 className='text-xl font-bold'>{title}</h3>
      <p className='text-sm'>{description}</p>
      <p className='text-sm'>Max Votes: {maxVotes}</p>
      {pollOptions.map((option, i) => (
        <div key={i} className='flex items-center'>
          <input type='checkbox' />
          <label>{option}</label>
        </div>
      ))}
    </div>
    
  )
}

export default Poll