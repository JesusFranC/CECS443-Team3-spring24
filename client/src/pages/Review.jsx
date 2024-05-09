import React, { useState } from 'react';

const Review = ({ onSubmit }) => {
  // State variables to store review title, text, and rating
  const [reviewTitle, setReviewTitle] = useState('');
  const [reviewText, setReviewText] = useState('');
  const [rating, setRating] = useState(0); // 0 represents no rating

  // Function to handle submission of the review
  const handleSubmit = (e) => {
    e.preventDefault();
    // Call the onSubmit function passed from the parent component
    onSubmit({ reviewTitle, reviewText, rating });
    // Clear the form after submission
    setReviewTitle('');
    setReviewText('');
    setRating(0);
  };

  return (
    <div className="mx-auto mt-6 w-4/5">
      <button className="home-button absolute top-3 right-10 p-4 "></button>
      <h2 className="text-4xl font-bold mb-4 mt-10">Write A Review:</h2>
      <form onSubmit={handleSubmit} className="space-y-4 flex flex-col">
        {/* Title Input */}
        <div>
          <input
            id="reviewTitle"
            type="text"
            value={reviewTitle}
            onChange={(e) => setReviewTitle(e.target.value)}
            placeholder="Enter review title..."
            className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:border-blue-500"
          />
        </div>
        {/* Review Text Input */}
        <textarea
          value={reviewText}
          onChange={(e) => setReviewText(e.target.value)}
          placeholder="Write your review here..."
          rows={10}
          className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:border-blue-500"
        />
        {/* Submit Button */}
        <div className="self-end">
          <button type="submit" className="btn-register">
            Post Your Review
          </button>
        </div>
      </form>
    </div>
  );
};

export default Review;
