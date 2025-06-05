import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-resgister',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './resgister.component.html',
  styleUrl: './resgister.component.css'
})
export class ResgisterComponent implements OnInit {
  cancelRegister = output<boolean>()
  model:any={};
  private accountService = inject(AccountService);
  toastr = inject(ToastrService)
  
  ngOnInit(): void {
    //console.log("userlist",this.userFromHome)
  }
  register(){
      this.accountService.registerUser(this.model).subscribe({
      next:response=>{
        console.log("Register successfully",response)
      },
      error:error=>this.toastr.error(error.error),
      complete:()=>console.log("completed")
      
    });
    this.cancel()
  }
  cancel(){
    this.cancelRegister.emit(false)
  }
}
