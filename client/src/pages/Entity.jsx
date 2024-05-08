import React from 'react';
import outpost from '../assets/outpost.jpg';

const Entity = () => {
  // Dummy data for reviews (to be replaced with actual data)
  const reviews = [
    { id: 1, user: 'John Doe', review: 'Great selection of snacks!' },
    { id: 2, user: 'Jane Smith', review: 'Convenient location and friendly staff.' },
    { id: 3, user: 'Alice Johnson', review: 'Could use more variety in school supplies.' },
  ];

  return (
    <div className="mx-auto p-10 flex flex-col md:flex-row">
      <div className="md:w-1/2 md:pr-8">
        {/* Image */}
        <div className="mb-4">
          <img src={outpost} alt="Entity Image" className="w-full h-auto" />
        </div>
        {/* Reviews */}
        <div>
          <h3 className="text-xl font-semibold mb-2">Reviews:</h3>
          {reviews.map(review => (
            <div key={review.id} className="border-b border-gray-200 py-2">
              <p className="text-gray-600">{review.review}</p>
            </div>
          ))}
        </div>
      </div>
      <div className="md:w-1/2">
        {/* Title */}
        <h2 className="text-2xl font-bold mb-4">The Outpost Convenience Store</h2>
        {/* Description */}
        <p className="text-lg mb-4"> Convenience store connected to the Outpost Grill located by the College of Engineering with a variety of fresh food, snacks, school supplies and more.</p>
        {/* Hours */}
        <p className="text-lg font-bold">Hours:</p>
        <ul className="list-disc pl-6 mb-4">
          <li>Mon-Thur: 8 am - 7 pm</li>
          <li>Friday: 8 am - 5 pm</li>
          <li>Sat-Sun: Closed</li>
        </ul>
        {/* Write Review Button */}
        <button className="btn-register mb-4">
          Write a Review
        </button>
      </div>
    </div>
  );
};

export default Entity;
