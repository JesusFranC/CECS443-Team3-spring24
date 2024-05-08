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
  const [poll, setPoll] = useState(null);
  const [voted, setVoted] = useState(false);
  const [selectedOption, setSelectedOption] = useState(false);
  const [selectedDescription, setSelectedDescription] = useState(''); 
  const createdAt = new Date();

  //useEffect to log updated selectedOption
  useEffect(() => {
    console.log(' in useEffect()...selectedOption: ', selectedOption);
  }, [selectedOption]);

  //useEffect to log updated selectedDescription
  useEffect(() => {
    console.log(' in useEffect()...selectedOptionDescriptoin: ', selectedDescription);
  }, [selectedDescription]);

  //useEffect to fetch poll id
  useEffect(() => {
    async function fetchPoll() {
      const url = 'http://localhost:5206/Poll/${id}'; //FIXME: Replace with URL for getting poll by ID
      try {
          const response = await fetch (url, {
              method: 'GET',
          });
          if (response.ok) {
              const data = await response.json();
              console.log('Poll ID: ', data);
              setPoll(data);
          } else {
              console.log('Poll not found');
          }
      } catch (error) {
          console.log('Error in fetching poll: ', error);
      }
    }
    fetchPoll();
  }, [setPoll]);

  //FIXME: hardcoded values for now
  title = 'what is the better convenience store?'
  description = 'just curious wat u guys think?? evaluating based on their snack options and how stocked they are.'
  pollOptions = ['Outpost Convenience Store', 'Bookstore Convenience Store']

  const handleVote = (event, option, index) => {
    event.preventDefault();
    console.log('in handleVote');
    if (validateForm()) {
      if ( index === 0) {
        setSelectedOption(true);
        setSelectedDescription(option);
      } else if (index === 1) { 
        setSelectedOption(false); 
        setSelectedDescription(option);
      }
      console.log('voted #', selectedOption, ' for ', selectedDescription)
      setVoted(true);
      handlePostVote(event, selectedOption);
    } 
  }
  //posting vote to backend
  const handlePostVote = async (e, option) => {
    e.preventDefault();
    //Implement submitting poll data to the server
    const url = 'http://localhost:5206/Poll/voteOnPoll'; //FIXME: Replace with URL for posting user vote
    try {
        console.log('In Handle post vote', {option});
        const response = await fetch (url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({option}) 
        });
        if (response.ok) {
            console.log('Vote posted', {option});
        } else {
            console.log('Vote failed. Reponse not ok. Please try again.')
        }
    } catch (error) {
        console.log('Error in poll voting: ', error);
    }
    console.log('voted: ', option);
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
            <button key={i} className='btn-poll' type='submit' onClick={(event) => handleVote(event, option, i)}>
              <label className='px-3'>{option}</label>
            </button>
          ))}
        </form>
        <div className='w-16 h-1.5 mt-3 bg-lbsu-blue rounded-lg'></div>
        <p className='text-sm py-3 italic'>Posted on {createdAt.toLocaleString()}</p>
        <p className='text-sm py-3 italic'>You voted for: {selectedDescription}</p>
      </div>
    </div>
    
  )
}

export default Poll