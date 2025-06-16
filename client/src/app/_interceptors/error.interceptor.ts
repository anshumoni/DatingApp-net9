import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router'
import { Toast, ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const toostr = inject(ToastrService)
  return next(req).pipe(
    catchError((error)=>{
      console.log(error.status)
      if(error){
        switch (error.status) {
          case 400:
            if(error.error.errors){
              const modelStateErrors =[]
            for(let key in error.error.errors){
              if(error.error.errors[key]){
                  modelStateErrors.push(error.error.errors[key]);
              }
            }
            throw modelStateErrors.flat();
            }
        break;
        case 401:
          toostr.error('Unauthirized',error.status);
          break;
        case 404:
          router.navigateByUrl('/not-found')  
          break;
         case 500:
          const navigationExtra:NavigationExtras = {state:{error:error.errors}} 
          router.navigateByUrl('/server-error',navigationExtra)
          break;
        default:
           toostr.error('Something unexpected went wrong')
            break;
        }
      }
      throw error;
    })
  );
};
