import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/Services/auth.service';
import { UserStoreService } from 'src/Services/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public role:string =""
  loginform! : FormGroup
  constructor(
    private fb : FormBuilder ,
    private myService : AuthService,
    private myRouter : Router,
    private toast : NgToastService ,
    private user_store : UserStoreService
    ){}

  ngOnInit(): void {

    this.loginform = this.fb.group({
      email : ['' , Validators.required],
      password : ['' , Validators.required],
    })
  }

  onsubmit(){

    if(this.loginform.valid){

      this.myService.Login(this.loginform.value).subscribe({

        next:(data)=>{

         // alert(data.message)
          this.myService.storeToken(data.token)

          let payload = this.myService.decodedToken();
          this.user_store.setFullNameForStore(payload.sub)
          this.user_store.setRoleForStore(payload.roles)

          this.user_store.getRoleFromStore().subscribe({
            next:(data)=>{

              let fullRoleFromToken = this.myService.getRoleFromToken();
              this.role = data || fullRoleFromToken
            }
          })

          if(this.role === 'Admin'){
            this.toast.success({detail:"SUCCESS",summary:'Login - Success',duration:5000});
            console.log("I AM  Admin")
            this.myRouter.navigate(['dashboard'])
          }
          else if(this.role === 'User'){
            this.toast.success({detail:"SUCCESS",summary:'Login - Success',duration:5000});

            this.myRouter.navigate(['Browse'])
          }
          else{
            this.myRouter.navigate(['login'])
          }

          // this.toast.success({detail:"SUCCESS",summary:'Login - Success',duration:5000});

          // this.checkRole()
          //this.myRouter.navigate(['Browse'])
        },
        error:(err)=>{
          // alert(err?.error.Message)
          this.toast.error({detail:"ERROR",summary:'Data inserted not correct',duration:5000});

        }
      })
    }
    else{
      this.validateAllFormField(this.loginform)
      this.toast.error({detail:"ERROR",summary:'Your Error Message',duration:5000});
    }
  }

  private validateAllFormField(formgroup : FormGroup){

    Object.keys(formgroup.controls).forEach(field=>{
      const control = formgroup.get(field);

      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true})

      }else if(control instanceof FormGroup){

        this.validateAllFormField(control)

      }
    })

  }

}


