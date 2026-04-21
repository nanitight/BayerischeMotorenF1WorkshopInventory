export interface GrandPrixResult{
    raceDay: string,
    location: string,
    pointsScored: number,
    positionInTeamGrid: number
}

export interface DbTracked{
    id:string,
}

export interface DbTrackedGrandPrixResult extends DbTracked,GrandPrixResult{}