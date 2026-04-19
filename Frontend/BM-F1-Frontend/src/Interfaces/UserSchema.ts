import {z} from 'zod'

export const userSchema = z.object({
    Username:z.string()
                .min(1,"Please enter username."),
    Password: z.string()
                .min(1,"Please enter password.")
})

export type LoginUser = z.infer<typeof userSchema> ;