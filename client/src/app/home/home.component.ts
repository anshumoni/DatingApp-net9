import { Component, inject, OnInit } from '@angular/core';
import { ResgisterComponent } from "../resgister/resgister.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ResgisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  implements OnInit{
  Isregusterform = false;
  http=inject(HttpClient)
  users:any;
   ngOnInit(): void {
     this.getUser();
    }

  registerForm(){
    this.Isregusterform = !this.Isregusterform;
  }


  getUser(){
      this.http.get("https://localhost:5001/api/users").subscribe({
       next:response=>this.users=response,
      error:error=>console.log(error),
      complete:()=>console.log("Requested Completed")
    
      })
    }
  cancelReg(event:boolean){
    this.Isregusterform = event
  }
}
