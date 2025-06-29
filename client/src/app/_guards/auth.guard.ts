import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  let accountService = inject(AccountService);
  const toastr = inject(ToastrService)
  if(accountService.currentuser()){
    return true;
  }else{
    toastr.error('You are not logedin');
    return false;
  }
};
