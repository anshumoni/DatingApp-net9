import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import User from '../_model/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseurl = "https://localhost:5001/api";

  currentuser = signal<User | null>(null)

  constructor(private http: HttpClient) {}


  loginUser(logindata: any) {
    return this.http.post<User>(this.baseurl + "/account/login", logindata,{withCredentials:true}).pipe(
      map(user=>{
        if(user){
          localStorage.setItem('user',JSON.stringify(user))
          this.currentuser.set(user)
        }
        
      })
    );
  }
 
  registerUser(registerdata: any) {
    return this.http.post<User>(this.baseurl + "/account/register", registerdata).pipe(
      map(user=>{
        if(user){
          localStorage.setItem('user',JSON.stringify(user))
          this.currentuser.set(user)
        }
        
      })
    );
  }
  logout(){
    localStorage.removeItem('user')
  }
}
