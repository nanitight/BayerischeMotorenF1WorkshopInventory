import {z} from 'zod'

export const userRegisterSchema = z.object({
    Username:z.string()
                .min(1,"Please enter username."),
    Password: z.string().trim()
                .min(8,"Please enter password with 8 characters or more."),
    ConfirmPassword: z.string().trim()
    
}).refine(data => { return data.Password === data.ConfirmPassword},{
    message: "Passwords must be the same.",
    path:["ConfirmPassword"]
})

export type RegisterUser = z.infer<typeof userRegisterSchema> ;