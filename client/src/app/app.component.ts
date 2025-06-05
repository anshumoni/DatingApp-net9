import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent,RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http=inject(HttpClient)
  accountService = inject(AccountService)

   title = 'Dating App';
   

    ngOnInit(): void {
     this.setcurrentUser();
    }

    setcurrentUser(){
       const user = localStorage.getItem('user')
       if(!user) return;
       this.accountService.currentuser.set(JSON.parse(user))
    }
    
 
}
