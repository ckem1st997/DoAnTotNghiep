export class User {
    id!: string ;
    username!: string;
    role!: number;
    token?: string;
}
export enum Role {
    User = 'User',
    Admin = 'Admin'
}