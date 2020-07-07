import { Component, OnInit, Input } from '@angular/core';
import { UserForTable } from 'src/app/models/UserForTable';
import { UserService } from 'src/app/services/ApiConnections/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  @Input() userTable:UserForTable;
  
  constructor(private userService: UserService) { }

  public myId:number = Number(sessionStorage.getItem('myId'));
  public followText;
  public buttonStyle;

  public SetButtonData(color:string){
    this.buttonStyle = {
      'background-color': color,
      'width':'200px'
    };
    if (color === 'white'){
      this.followText= 'You';
    } else if (color === 'orange'){
      this.followText= 'follow';
    } else if (color === 'green'){
      this.followText= 'following';
    }else if (color === 'red'){
      this.followText= 'unfollow';
    }
  }

  

  ngOnInit() {
    if (this.userTable.userID==this.myId){
      this.SetButtonData('white');
    }
    else if (this.userTable.followedByMe===false){
      this.SetButtonData('orange');
    } else{
      this.SetButtonData('green');
    }
  }

  public DoCommand(){
    if (this.userTable.followedByMe===false){
      this.userService.AddFollower(this.userTable.userID);
    } else{
      this.userService.DeleteFollower(this.userTable.userID);
    }
  }

  public changeText(change:boolean){
    if (change=== true){
      if (this.followText=== 'following'){
        this.SetButtonData('red');
      }
    } else {
      if (this.followText==='unfollow'){
        this.SetButtonData('green');
      }
    }
  }
}
