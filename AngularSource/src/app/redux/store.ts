import { UserForTable } from '../models/UserForTable';

export class Store{
    public usersTable:UserForTable[] = [];
    public AddFollowerError:string;
    public DeleteFollowerError:string;
    public GetUsersForTableError:string;
    
    public GetOneUserForTableByIdError:string;
    

}