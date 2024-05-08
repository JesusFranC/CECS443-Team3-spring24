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

  const handleVote = (event, option) => {
    event.preventDefault();
    console.log('in handleVote');
    if (validateForm()) {
      setSelectedOption(option);
      console.log('voted', option)
      setVoted(true);
      handlePostVote(event);
    } 
  }

  const handlePostVote = async (e) => {
    e.preventDefault();
    //Implement submitting poll data to the server
    const url = 'http://localhost:5206/Registeration/Register'; //FIXME: Replace with URL for posting user vote
    try {
        console.log('In Handle post vote');
        const response = await fetch (url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({selectedOption}) 
        });
        if (response.ok) {
            console.log('Vote posted', {selectedOption});
        } else {
            alert('Voting failed. Please try again.');
        }
    } catch (error) {
        console.log('Error in poll voting: ', error);
    }
    console.log('voted: ', selectedOption);
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
        <form className='flex flex-col' >
          {pollOptions.map((option, i) => (
            <button key={i} className='btn-poll' type='submit' onClick={(event) => handleVote(event, option)}>
              <label className='px-3'>{option}</label>
            </button>
          ))}
        </form>
        <div className='w-16 h-1.5 mt-3 bg-lbsu-blue rounded-lg'></div>
        <p className='text-sm py-3 italic'>Posted on {createdAt.toLocaleString()}</p>
        <p className='text-sm py-3 italic'>You voted for: {selectedOption}</p>
      </div>
    </div>
    
  )
}

export default Poll