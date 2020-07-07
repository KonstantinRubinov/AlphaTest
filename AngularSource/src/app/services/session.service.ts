import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../models/User';
import { CookieService } from 'ngx-cookie-service';
import { LogService } from './log.service';
import { loginUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  public constructor(private http: HttpClient,
                     private logger: LogService,
                     private cookieService: CookieService) { 
  }
  
  public login(): any {
    let user:User = this.getCookie();
    const body = new HttpParams()          
    .set('grant_type', 'password')          
    .set('username', user.userName)    
    .set('password', user.userPassword)    

    return this.http.post(loginUrl, body.toString(), {observe: 'response',    
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },    
    });
  }

  public setCookie():void{
    if (!this.cookieService.check('AlphaTest')){
      let expire = new Date();
      var time = Date.now() + ((3600 * 1000) * 6);
      expire.setTime(time);
  
      let user: any; //new object declaration
      user = {"userID": 1, "userName": "user01", "userGroupId": 1, "userNumberOfFollowers": 0, "userPassword": "user01"}
      this.logger.debug("setCookie: ", user);
      this.cookieService.set('AlphaTest', JSON.stringify(user), expire);
    }
  }

  public getCookie():User{
    let value:any;
    if (this.cookieService.check('AlphaTest')){
      value = this.cookieService.get('AlphaTest');
      this.logger.debug("getCookie: ", value);
      value = JSON.parse(value);
      let user:User = new User(value.userID, value.userName, value.userGroupId, value.userNumberOfFollowers, value.userPassword);
      this.logger.debug("getCookie: ", user);
      return user;
    }
    this.logger.error("getCookie: ", 'No Cookie');
  }
  
}
