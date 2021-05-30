import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model : any ={};
  @Output() cancelRegister = new EventEmitter();
  constructor(private accountService: AccountService, private toastService: ToastrService) { }

  ngOnInit(): void {
  }

  Register(){
    this.accountService.register(this.model).subscribe(
      (res)=>{
        this.toastService.success('registration successfully done');
        this.cancel();
      },
      (error)=>{
        console.log(error);
        this.toastService.error(error.error);
      }
    );



  }

  cancel(){
    this.cancelRegister.emit(false);
  }

}
