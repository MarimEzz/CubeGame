import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/Services/auth.service';
import { UserStoreService } from 'src/Services/user-store.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit{

  // signupForm! : FormGroup
  repeatPass : string = 'none'
  constructor(private myService : AuthService ,
     private myRouter : Router ,
      private toast : NgToastService, private user_store : UserStoreService){}

  signupForm = new FormGroup({

    firstname : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
    lastname : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
    email : new FormControl("" , [Validators.required , Validators.email]),
    PWD : new FormControl("" , [Validators.required ,  Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]),
    rpwd : new FormControl(""),
    username : new FormControl("" , [Validators.required])
  });

  ngOnInit(): void {
  }

  onSignup(){

    if(this.Password.value === this.RPassword.value){
      console.log("summited")
      console.log(this.signupForm.valid)
      this.repeatPass = 'none'

    if(this.signupForm.valid){

      const person = Object.create(null);
      person.firstName =  this.signupForm.value.firstname
      person.lastName = this.signupForm.value.lastname
      person.username = this.signupForm.value.username
      person.email = this.signupForm.value.email
      person.password = this.signupForm.value.PWD

      console.log(person)
      this.myService.signUp(person).subscribe({
        next:(data)=>{

          this.myService.storeToken(data.token)
          let payload = this.myService.decodedToken();
          this.user_store.setFullNameForStore(payload.sub)
          this.toast.success({detail:"SUCCESS",summary:'User Registered Successfully',duration:5000});
          this.signupForm.reset()
          this.myRouter.navigate(['Browse'])
        },
        error:(err)=>{
          this.toast.error({detail:"ERROR",summary:'Data inserted not Correct',duration:5000});

        }
      })

    }
    else{
     // this.validateAllFormField(this.signupForm)
     this.toast.error({detail:"ERROR",summary:'Your Error Message',duration:5000});
    }
  }else{

      this.repeatPass = 'inline'
  }
  }

  get FirstName() : FormControl{
    return this.signupForm.get("firstname") as FormControl;
  }

  get LastName() : FormControl{
    return this.signupForm.get("lastname") as FormControl;
  }

  get Email() : FormControl{
    return this.signupForm.get("email") as FormControl;
  }

  get Password() : FormControl{
    return this.signupForm.get("PWD") as FormControl;
  }

  get RPassword() : FormControl{
    return this.signupForm.get("rpwd") as FormControl;
  }

  get username(){
    return this.signupForm.get("username") as FormControl;
  }
}
