import type { DbTrackedGrandPrixResult,  } from '../../Interfaces/GrandPrixResult'

interface ResultTableProps{
result : DbTrackedGrandPrixResult, 
deleteFunc : (id:string)=>Promise<void>
}

export default function  ResultsTable({
    result,deleteFunc
} : ResultTableProps) {
  return (
     <tr className="hover:bg-base-300">
        <th>1</th>
        <td>{result.raceDay}</td>
        <td>{result.location}</td>
        <td>{result.pointsScored}</td>
        <td>{result.positionInTeamGrid}</td>
        <td>
            <button  className="btn btn-sm btn-ghost tooltip text-green-300" data-tip="Edit Item">
                <label>
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-4 h-4"><path strokeLinecap="round" strokeLinejoin="round" d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 18.07a4.99 4.99 0 0 1-1.308 1.493l-4.137 1.15s-2.585.72-2.906-.328c-.321-1.048 1.15-2.585 1.15-2.585l1.15-4.137a4.99 4.99 0 0 1 1.493-1.308L16.862 4.487Z" /></svg>
                </label>
            </button>
            
            <button onClick={async ()=> await deleteFunc(result.id)} className="btn btn-sm btn-ghost tooltip text-error" data-tip="Delete Item">
                <label>
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-4 h-4"><path strokeLinecap="round" strokeLinejoin="round" d="m14.74 9-.346 9m-4.788 0-.91-6.75m1.527-6.524 5.22-.647 1.696.182-5.22.647M6.88 12l.74 4.5M6 9h12" /></svg>
                </label>
            </button>
        </td>
    </tr>
  )
}
