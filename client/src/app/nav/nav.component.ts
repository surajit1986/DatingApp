import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user.model';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  public model : any={};
  
  constructor(public accountSevice: AccountService) { }

  ngOnInit(): void {

  }

  login(){
    this.accountSevice.login(this.model).subscribe(response => {

        console.log(response);

    }, error =>{

    });

  }

  logout(){
    this.accountSevice.logout();
}



}
