import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { LogService } from '.././log.service';
import { follow, unfollow, fortable } from 'src/environments/environment';
import { UserForTable } from '../../models/UserForTable';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public constructor(private http: HttpClient,
                     private redux: NgRedux<Store>,
                     private logger: LogService) { 
  }
  
  public AddFollower(followedId:number): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<UserForTable>(follow+followedId, { headers: he });
    observable.subscribe(userFollowed=>{
      const action: Action={type:ActionType.UpdateUsersTable, payload:userFollowed};
      this.redux.dispatch(action);
      this.logger.debug("AddFollower: ", userFollowed);
    }, error => {
      const action: Action={type:ActionType.AddFollowerError, payload:error.message};
      this.redux.dispatch(action);
      alert ("AddFollowerError: " + error.message);
      this.logger.error("AddFollowerError: ", error.message);
    });
  }

  public DeleteFollower(followedId:number): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<UserForTable>(unfollow+followedId, { headers: he });
    observable.subscribe(userFollowed=>{
      const action: Action={type:ActionType.UpdateUsersTable, payload:userFollowed};
      this.redux.dispatch(action);
      this.logger.debug("DeleteFollower: ", userFollowed);
    }, error => {
      const action: Action={type:ActionType.DeleteFollowerError, payload:error.message};
      this.redux.dispatch(action);
      alert ("DeleteFollowerError: " + error.message);
      this.logger.error("DeleteFollowerError: ", error.message);
    });
  }
  
  public GetUsersForTable(): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<UserForTable[]>(fortable, { headers: he });
    observable.subscribe(UsersForTable=>{
      const action: Action={type:ActionType.GetUsersForTable, payload:UsersForTable};
      this.redux.dispatch(action);
      this.logger.debug("GetUsersForTable: ", UsersForTable);
    }, error => {
      const action: Action={type:ActionType.GetUsersForTableError, payload:error.message};
      this.redux.dispatch(action);
      alert ("GetUsersForTableError: " + error.message);
      this.logger.error("GetUsersForTableError: ", error.message);
    });
  }
  
  public GetOneUserForTableById(followedId:number): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<UserForTable>(fortable+followedId, { headers: he });
    observable.subscribe(userForTable=>{
      const action: Action={type:ActionType.GetOneUserForTableById, payload:userForTable};
      this.redux.dispatch(action);
      this.logger.debug("GetOneUserForTableById: ", userForTable);
    }, error => {
      const action: Action={type:ActionType.GetOneUserForTableByIdError, payload:error.message};
      this.redux.dispatch(action);
      alert ("GetOneUserForTableByIdError: " + error.message);
      this.logger.error("GetOneUserForTableByIdError: ", error.message);
    });
  }
}