import React, { useState, useEffect, useContext }  from 'react';
import { AuthContext } from '../context/AuthProvider';

//USE CASE #4: I want to view reviews and ratings for different campus entities, so
//I can make informed decisions about my campus experiences.
export const Poll = ({ title, description, pollOptions = [] }) => {
  //for pollingModel
  //FIXME: To-DO: 
  //1. persistence = get user from context
  //2. handleVote = send poll response to backend
  //3. button - mark as selected
  //4. Limit vote to 1 per user
  const {authUser, setAuthUser} = useContext(AuthContext);
  const [user, setUserDetails] = useState(null);
  const [voted, setVoted] = useState(false);
  const [selectedOption, setSelectedOption] = useState('');
  const createdAt = new Date();

  //FIXME: hardcoded values for now
  title = 'Best convenience store?'
  description = 'just curious wat u guys think?? evaluating based on their snack options and how stocked they are.'
  pollOptions = ['Outpost Convenience Store', 'Bookstore Convenience Store', 'Caffiene Lab', 'WallStrEAT Cafe']

  const handleVote = async (e) => {
    e.preventDefault();
    console.log('in handleVote');
    if (validateForm()) {
      setVoted(true);
      setSelectedOption(`${e.target.value}`);
      console.log('voted', selectedOption)
    } 
  }

  const validateForm = () => {
    if (voted === true) {
        alert('You have already voted on this poll. You cannot vote more than once');
        return false;
    } 
    return true;
  }

  return (
    <div className="font-sans w-[520px] h-[586px] bg-light-grey-1 rounded-2xl">
      <div className="flex flex-col pt-20 px-12">
        <h3 className='text-3xl font-semibold pb-4'>{title}</h3>
        <p className='text-sm py-4'>{description}</p>
        <form className='flex flex-col' onSubmit={handleVote}>
          {pollOptions.map((option, i) => (
            <button key={i} className='btn-poll' type='submit'>
              <label className='px-3'>{option}</label>
            </button>
          ))}
        </form>
        <div className='w-16 h-1.5 mt-3 bg-lbsu-blue rounded-lg'></div>
        <p className='text-sm py-3 italic'>Posted on {createdAt.toLocaleString()}</p>
      </div>
    </div>
    
  )
}

export default Poll