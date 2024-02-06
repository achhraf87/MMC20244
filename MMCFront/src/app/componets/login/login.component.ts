import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import Validateform from '../helpers/Validateform';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { noop } from 'rxjs';
import { EtudiantStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  nom:string ="";

  type: string ="password";
  isText : boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup

  constructor(private fb: FormBuilder,
     private auth: AuthService,private router:Router,
     private toast: NgToastService,
     private emailStore:EtudiantStoreService){}

  ngOnInit(): void{
    this.loginForm = this.fb.group({
      Email:['',Validators.required],
      Password:['',Validators.required]

    })

  }

  hideShowPass(){

    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type ="text" : this.type ="password";

  }
  Connexion(){
    if(this.loginForm.valid){
      console.log(this.loginForm.value)
      this.auth.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          this.toast.success({detail:'SUCCESS', summary:res.message, duration: 5000});
          this.loginForm.reset();
          this.auth.storeToken(res.token);
          const tokenPayload = this.auth.decodeToken();
          this.emailStore.setEmailFromStore(tokenPayload.unique_name)
          this.emailStore.setRoleFromStore(tokenPayload.role)
          this.router.navigate(['dashboard']);
        },
        error:(err)=>{
          this.toast.error({detail:'Désolé', summary:"Les informations sont incorrecte", duration: 5000});
        }
      })
    }
    else{

      Validateform.validateAllFormFields(this.loginForm);

    }
  }
  
 
}
