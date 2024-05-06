import React, { useState, useEffect }  from 'react'

//USE CASE #4: I want to view reviews and ratings for different campus entities, so
//I can make informed decisions about my campus experiences.
export const Poll = ({ Title, description, maxVotes, pollOptions = [], UserAccount_UID }) => {
  Title = 'Best convenience store?'
  description = 'just curious wat u guys think?? evaluating based on their snack options and how stocked they are.'
  maxVotes = 10
  pollOptions = ['Outpost Convenience Store', 'Bookstore Convenience Store', 'Caffiene Lab', 'WallStrEAT Cafe']
  
  //for pollingModel
  const PollID = '1234';
  UserAccount_UID = 'user_123';
  const createdAt = new Date();

  return (
    <div className="font-sans w-[520px] h-[586px] bg-light-grey-1 rounded-2xl">
      <div className="flex flex-col pt-20 px-12">
        <h3 className='text-3xl font-semibold pb-4'>{Title}</h3>
        <p className='text-sm py-4'>{description}</p>
        <form className='flex flex-col'>
        {pollOptions.map((option, i) => (
          <button key={i} className='btn-poll' type='submit'>
            <label className='px-3'>{option}</label>
          </button>
        ))}
        </form>
        <p className='text-sm py-4'>Max Votes: {maxVotes}</p>
        <div className='w-16 h-1.5 mt-3 bg-lbsu-blue rounded-lg'></div>
        <p className='text-sm py-3 italic'>Posted on {createdAt.toLocaleString()}</p>
      </div>
    </div>
    
  )
}

export default Poll