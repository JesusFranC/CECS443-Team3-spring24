import React, { useState, useEffect }  from 'react'

export const Polls = () => {
  return (
    <div className='h-full flex flex-auto'> this is where we'll view all Polls
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