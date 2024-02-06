import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import Validateform from '../helpers/Validateform';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {

  espace : string = " ------------------  ";
  type: string ="password";
  isText : boolean = false;
  eyeIcon: string = "fa-eye-slash";
  signUpForm!: FormGroup;


  constructor(private fb: FormBuilder, private auth : AuthService,private router: Router,private toast:NgToastService){}

  ngOnInit(): void{
    this.signUpForm = this.fb.group({

      Fullname:['',Validators.required],
      Email:['',Validators.required],
      ImageUrl:['',Validators.required],
      Password:['',Validators.required],
      MCT: ['', Validators.required],
      MVP: ['', Validators.required],
      Role: ['', Validators.required],
      Gender: ['', Validators.required]
      
    });

    this.signUpForm.patchValue({
      MCT: 'valeur_MCT',
      MVP: 'valeur_MVP',
      Role: 'valeur_Role',
      Gender: 'valeur_Gender',
    });
  }

  

  hideShowPass(){

    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type ="text" : this.type ="pass";

  }

  onSignUp(){
    if(this.signUpForm.valid){
      //perform logic for signUp
      this.auth.signUp(this.signUpForm.value)
      .subscribe({
        next:(res =>{
          console.log(res)
          alert(res.message)
          this.toast.success({detail:'Bingo', summary:res.message, duration: 5000});
          this.signUpForm.reset();
          this.router.navigate(['login']);
        }),
        error:(err =>{
          this.toast.warning({detail:'Attention', summary:"Ce mail est déja existe!", duration: 5000});
        })
      })
      console.log(this.signUpForm.value)

    }
    else{
      Validateform.validateAllFormFields(this.signUpForm)
      //logic for throwing error
    }
  }



}
