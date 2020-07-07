import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { Store } from 'src/app/redux/store';
import { Unsubscribe } from 'redux';
import { SessionService } from 'src/app/services/session.service';
import { UserService } from 'src/app/services/ApiConnections/user.service';
import { UserForTable } from 'src/app/models/UserForTable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(private sessionService:SessionService, private userService: UserService, private redux:NgRedux<Store>) {}
  public usersTable: UserForTable[]=[];
  private unsubscribe:Unsubscribe;


  public ngOnInit(): void {
    this.sessionService.setCookie();
    this.signIn();
    
    this.unsubscribe = this.redux.subscribe(()=>{
      this.usersTable = this.redux.getState().usersTable;
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe();
  }


  errmsg: any;
  public signIn(): void {

    this.sessionService.login() 
                     .subscribe(res => {    
                       if (res.status === 200) { 
                        //this.successmsg = 'token - ' + res.body.access_token; 
                        sessionStorage.setItem('access_token', res.body.access_token);
                        sessionStorage.setItem('myId', res.body.myId);
                        this.userService.GetUsersForTable();
                              // const action: Action={type:ActionType.UserLogin};
                              // this.redux.dispatch(action);                                                                                    localStorage.setItem('access_token', res.body.access_token);  
                        } else {  
                          this.errmsg = res.status + ' - ' + res.statusText;
                          alert ("login: " + this.errmsg);
                          
                          // const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                          // this.redux.dispatch(action);
                          }  
                         },  
                       err => {                                 
                        if (err.status === 401  ) {  
                          this.errmsg = err.status + ' - ' + err.statusText;
                          alert ("login: " + this.errmsg);
                          // const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                          // this.redux.dispatch(action); 
                           }   
                          else if (err.status === 400  ) {  
                            this.errmsg = err.status + ' - ' + err.statusText;
                           alert ("login: " + this.errmsg); 
                          //  const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                          // this.redux.dispatch(action);
                          }   
                          else {  
                            this.errmsg = err.status + ' - ' + err.statusText;
                          alert ("login: " + this.errmsg); 
                          // const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                          // this.redux.dispatch(action);
                           }  
                        });  
    }
  
}
