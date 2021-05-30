import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(public accountSevice: AccountService, private router: Router,
    private toastService: ToastrService) { }

  ngOnInit(): void {

  }

  login(){
    this.accountSevice.login(this.model).subscribe(response => {

        this.router.navigateByUrl('members');
        this.toastService.success('logged in successfully');

    }, error =>{
      this.router.navigateByUrl('home');
      this.toastService.error(error.error);
    });

  }

  logout(){
    this.accountSevice.logout();
    this.router.navigateByUrl('home');
}



}
