import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {

  error:any; 
  constructor(private route:Router){
      const navigation =this.route.getCurrentNavigation()
      this.error = navigation?.extras?.state?.['error'];
  }

}
