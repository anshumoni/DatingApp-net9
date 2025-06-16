import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css'
})
export class TestErrorComponent {
  private BASE_URL ="https://localhost:5001/api/"
  private http = inject(HttpClient)
   validationError:string[] =[];
  
 get400error(){
    this.http.get(this.BASE_URL+'buggy/auth').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get404error(){
    this.http.get(this.BASE_URL+'buggy/not-found').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get401error(){
    this.http.get(this.BASE_URL+'buggy/bad-request').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get500error(){
    this.http.get(this.BASE_URL+'buggy/server-error').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  getValidationerror(){
    this.http.post(this.BASE_URL+'account/register',{}).subscribe({
      next:response=>console.log(response),
      error:error=>{
        console.log(error)
        this.validationError = error;
      }
    })
  }

}
