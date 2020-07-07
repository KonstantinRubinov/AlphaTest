import { LogService } from '../services/log.service';

export class User {
    private logger: LogService = new LogService();

    private _userID: number;
    private _userName: string;
    private _userGroupId: number;
    private _userNumberOfFollowers: number;
    private _userPassword: string;

    public constructor(
        userID?: number,
        userName?: string,
        userGroupId?: number,
        userNumberOfFollowers?: number,
        userPassword?: string
    ) {
        this.userID = userID;
        this.userName = userName;
        this.userGroupId = userGroupId;
        this.userNumberOfFollowers = userNumberOfFollowers;
        this.userPassword = userPassword;
     }

    get userID():number{
        this.logger.debug("get userID: ", this._userID);
        return this._userID;
    }

    set userID(val){
        this._userID=val;
        this.logger.debug("set userID: ", this._userID);
    }

    get userName():string{
        this.logger.debug("get userName: ", this._userName);
        return this._userName;
    }

    set userName(val){
        this._userName=val;
        this.logger.debug("set userName: ", this._userName);
    }

    get userGroupId():number{
        this.logger.debug("get userGroupId: ", this._userGroupId);
        return this._userGroupId;
    }

    set userGroupId(val){
        this._userGroupId=val;
        this.logger.debug("set userGroupId: ", this._userGroupId);
    }

    get userNumberOfFollowers():number{
        this.logger.debug("get userNumberOfFollowers: ", this._userNumberOfFollowers);
        return this._userNumberOfFollowers;
    }

    set userNumberOfFollowers(val){
        this._userNumberOfFollowers=val;
        this.logger.debug("set userNumberOfFollowers: ", this._userNumberOfFollowers);
    }

    get userPassword():string{
        this.logger.debug("get userPassword: ", this._userPassword);
        return this._userPassword;
    }

    set userPassword(val){
        this._userPassword=val;
        this.logger.debug("set userPassword: ", this._userPassword);
    }
}