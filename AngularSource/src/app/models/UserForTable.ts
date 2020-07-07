import { LogService } from '../services/log.service';

export class UserForTable {
    private logger: LogService = new LogService();
    
    private _userID: number;
    private _userName: string;
    private _groupName: string;
    private _userNumberOfFollowers: number;
    private _followedByMe: boolean;

    public constructor(
        userID?: number,
        userName?: string,
        groupName?: string,
        userNumberOfFollowers?: number,
        followedByMe?: boolean
    ) { 
        this.userID = userID;
        this.userName = userName;
        this.groupName = groupName;
        this.userNumberOfFollowers = userNumberOfFollowers;
        this.followedByMe = followedByMe;
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

    get groupName():string{
        this.logger.debug("get groupName: ", this._groupName);
        return this._groupName;
    }

    set groupName(val){
        this._groupName=val;
        this.logger.debug("set groupName: ", this._groupName);
    }

    get userNumberOfFollowers():number{
        this.logger.debug("get userNumberOfFollowers: ", this._userNumberOfFollowers);
        return this._userNumberOfFollowers;
    }

    set userNumberOfFollowers(val){
        this._userNumberOfFollowers=val;
        this.logger.debug("set userNumberOfFollowers: ", this._userNumberOfFollowers);
    }

    get followedByMe():boolean{
        this.logger.debug("get followedByMe: ", this._followedByMe);
        return this._followedByMe;
    }

    set followedByMe(val){
        this._followedByMe=val;
        this.logger.debug("set followedByMe: ", this._followedByMe);
    }
}