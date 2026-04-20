import React from 'react'


export default function AddGrandPrixResult() {
  return (
    
    <div className='flex items-center justify-center mt-5'>
        <div className="card card-border bg-base-100 w-96">
            <div className="card-body">
                <h2 className="card-title">Add Race Result</h2>
                <form>
                    <label className='input'>
                        <input  type="text" className="input " placeholder="Location"/>
                    </label>
                </form>
                <div className="card-actions justify-end">
                <button className="btn btn-primary">Add</button>
                </div>
            </div>
        </div>
    </div>
  )
}
