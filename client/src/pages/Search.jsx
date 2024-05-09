import React, { useState } from 'react';

const EntityList = () => {
  // Dummy data for entities (to be replaced with actual data)
  const [entities, setEntities] = useState([
    { id: 1, name: 'Entity 1', description: 'Description for Entity 1' },
    { id: 2, name: 'Entity 2', description: 'Description for Entity 2' },
    { id: 3, name: 'Entity 3', description: 'Description for Entity 3' },
    // Add more entities as needed
  ]);

  // State for search query
  const [searchQuery, setSearchQuery] = useState('');

  // Filter entities based on search query
  const filteredEntities = entities.filter(entity =>
    entity.name.toLowerCase().includes(searchQuery.toLowerCase())
  );

  // Function to handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    // You can perform additional actions here, such as submitting the search query to a backend server
  };

  // Function to handle entity click
  const handleEntityClick = (entityId) => {
    console.log('Entity clicked:', entityId);
    // Perform actions when an entity is clicked, such as displaying details or navigating to a new page
  };

  return (
    <div className="container mx-auto p-4">
      <button className="home-button absolute top-3 right-10 p-4 "></button>
      {/* Search Bar */}
      <form onSubmit={handleSubmit} className="flex items-center mt-10 mb-4">
        <input
          type="text"
          placeholder="Search Entities..."
          value={searchQuery}
          onChange={e => setSearchQuery(e.target.value)}
          className="w-full p-2 rounded border border-gray-300 mr-2"
        />
        <button type="submit" className="btn-register">
          Search
        </button>
      </form>

      {/* Entity List */}
      <div>
        {filteredEntities.map(entity => (
          <button
            key={entity.id}
            className="border-b border-gray-300 p-2 w-full text-left"
            onClick={() => handleEntityClick(entity.id)}
          >
            <h2 className="text-lg font-semibold">{entity.name}</h2>
            <p className="text-gray-600">{entity.description}</p>
          </button>
        ))}
      </div>
    </div>
  );
};

export default EntityList;
