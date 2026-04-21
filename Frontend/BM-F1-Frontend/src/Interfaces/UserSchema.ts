import {z} from 'zod'

export const userSchema = z.object({
    Username:z.string()
                .min(1,"Please enter username."),
    Password: z.string()
                .min(8,"Please enter password with 8 characters or more."),
})

export type LoginUser = z.infer<typeof userSchema> ;