import { Store } from "./store";
import { Action } from "./action";
import { ActionType } from "./action-type";
import { UserForTable } from '../models/UserForTable';

export class Reducer{
    public static reduce(oldStore: Store, action:Action):Store{
        let newStore:Store = {...oldStore};
        switch(action.type){
            case ActionType.UpdateUsersTable:
                let updateUsersTable = new UserForTable(action.payload.userID, action.payload.userName, action.payload.groupName, action.payload.userNumberOfFollowers, action.payload.followedByMe);
                let itemIndex = newStore.usersTable.findIndex(item => item.userID === updateUsersTable.userID);
                    newStore.usersTable[itemIndex] = updateUsersTable;
                break;
            case ActionType.AddFollowerError:
                newStore.AddFollowerError=action.payload;
                break;
            case ActionType.DeleteFollowerError:
                newStore.DeleteFollowerError=action.payload;
                break;
                
            case ActionType.GetUsersForTable:
                newStore.usersTable=action.payload;
                newStore.usersTable = action.payload.map(x => new UserForTable(x.userID, x.userName, x.groupName, x.userNumberOfFollowers, x.followedByMe));
                break;
            case ActionType.GetUsersForTableError:
                newStore.GetUsersForTableError=action.payload;
                break;
                
            case ActionType.GetOneUserForTableById:
                let getOneUserForTableById = new UserForTable(action.payload.userID, action.payload.userName, action.payload.groupName, action.payload.userNumberOfFollowers, action.payload.followedByMe);
                itemIndex = newStore.usersTable.findIndex(item => item.userID === getOneUserForTableById.userID);
                newStore.usersTable[itemIndex] = getOneUserForTableById;
                break;
            case ActionType.GetUsersForTableError:
                newStore.GetUsersForTableError=action.payload;
                break;
        }
        return newStore;
    }
}