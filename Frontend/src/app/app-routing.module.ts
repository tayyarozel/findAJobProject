import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { JobsComponent } from './components/jobs/jobs.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';

const routes: Routes = [
  {path:"", pathMatch:"full", component:HomeComponent},//ana sayfam
  {path:"jobs/:search",component:JobsComponent},
  {path:"jobs",component:JobsComponent},
  {path:"login",component:LoginComponent},
  {path:"signup",component:SignupComponent},
  //{path:"products/category/:categoryId",component:ProductComponent},// sana böyle bir sey göndericem burdakı "categoryId" al ve ilgili componenti değiştir
  //{path:"product/add",component:ProductAddComponent,canActivate:[LoginGuard]},// sana böyle bir sey göndericem burdakı "categoryId" al ve ilgili componenti değiştir
  //{path:"login",component:LoginComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
