import {z} from 'zod'

export const grandPrixResultSchema = z.object({
    RaceDay: z.date()
                .max(new Date(),"Race day cannot be in the futurre") ,
               // .transform((d)=> d.toISOString().split('T')[0]) , 
    Location: z.string()
                .min(3,"Location name must be 3 or more characters") , 
    
    PointsScored : z.number()
                    .min(0,"Cannot have negative points")
                    .max(43,"Cannot score more than 43 points in a day"),

    PositionInTeamGrid : z.number()
                    .min(1,"Team position cannot be less than 0.")
                    .max(10,"Only 10 teams can participate."),

})

export type GrandPrixResultZod = z.infer<typeof grandPrixResultSchema> ;
