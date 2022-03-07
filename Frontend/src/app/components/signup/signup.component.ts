import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm:FormGroup;
  constructor(private formBuilder:FormBuilder,private authService:AuthService,private toastrService:ToastrService,private router:Router) { }

  ngOnInit(): void {
    this.createSignupForm();
  }

  createSignupForm(){
    this.signupForm=this.formBuilder.group({
      firstName:["",Validators.required],
      lastName:["",Validators.required],
      email:["",Validators.required],
      password:["",Validators.required]
    })
  }

  signup(){
    if(this.signupForm.valid){
      let signupModel=Object.assign({},this.signupForm.value)
      this.authService.signup(signupModel).subscribe(response=>{
          this.toastrService.success("Kayıt Başarılı")
          localStorage.setItem("token",response.data.token);
          this.router.navigate(["/"])
      },responseError=>{
          this.toastrService.error(responseError.error)
      })
    }
  }

}
