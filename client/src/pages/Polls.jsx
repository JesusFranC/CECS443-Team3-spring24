import React, { useState, useEffect }  from 'react'

export const Polls = () => {
    const [polls, setPolls] = useState([]);
    
    function getPolls() {
        const url = 'http://localhost:5000/api/Poll';
        fetch (url, {
            method: 'GET',
        })
        .then(response => response.json())
        .then(pollsFromServer => {
            console.log('Polls from Server: ', pollsFromServer);
            setPolls(pollsFromServer);
        })
        .catch((error) => {
            console.log(error);
            alert(error);
        });
    }

  return (
        <div className='container mx-auto'>
            <div className='flex flex-row flex-auto h-full'>
                <div className='flex flex-col flex-auto justify-center items-center'>
                    <div>
                        <h1 className='text-3xl font-semibold'>Polls</h1>
                        <div className='mt-5 justify-end'>
                            <button onClick={getPolls} className='btn-view px-10'>View Polls</button>
                            {/* <button  className='btn-create mt-4'> Create Poll </button> */}
                        </div>
                        {renderPollTable()}
                    </div>
                </div>
            </div>
        </div>
  )
}

function renderPollTable() {
    return(
        <div className='w-full h-full flex flex-auto justify-items-center'>
        <table class="table-auto" className='m-10'>
            <thead>
                <tr>
                    <th class="px-4 py-2">PollID</th>
                    <th class="px-4 py-2">Title</th>
                    <th class="px-4 py-2">View Poll</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="border px-4 py-2">1</td>
                    <td class="border px-4 py-2">Best Convenience Store?</td>
                    <td class="border px-4 py-2">
                        <button className='btn-view'> View Poll</button>
                    </td>
                </tr>
                <tr>
                    <td class="border px-4 py-2">2</td>
                    <td class="border px-4 py-2">Best Study Spot?</td>
                    <td class="border px-4 py-2">
                        <button className='btn-view'> View Poll</button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
    )
}

export default Polls