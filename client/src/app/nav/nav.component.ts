import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  
  accountService = inject(AccountService);
  private router = inject(Router);
  toast = inject(ToastrService)
  model:any={};
 
  login(){
    this.accountService.loginUser(this.model).subscribe({
      next:()=> this.router.navigateByUrl('/memberlist'),
      error:error=>this.toast.error(error.error),
      complete:()=>console.log("completed")
      
    });
    console.log("login data",this.model)
  }
  
  logout(){
    this.accountService.logout();
    this.accountService.currentuser.set(null)
    this.router.navigateByUrl('/')
  }
}
