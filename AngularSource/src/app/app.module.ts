import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgReduxModule, NgRedux } from 'ng2-redux';
import { AppComponent } from './components/app-component/app.component';
import { CommonModule } from '@angular/common';
import { Store } from './redux/store';
import { Reducer } from './redux/reducer';
import { UserComponent } from './components/user/user.component';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgReduxModule,
    CommonModule
  ],
  providers: [CookieService ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  public constructor(redux:NgRedux<Store>){
    redux.configureStore(Reducer.reduce, new Store());
  }
}
