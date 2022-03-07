import { TokenModel } from './../models/tokenModel';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from './../models/loginModel';
import { Injectable } from '@angular/core';
import { SingleResponseModel } from '../models/singleResponseModel';
import { LocalStorageService } from './local-storage.service';
import { RegisterModel } from '../models/registerModel';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl="https://localhost:44335/api/auth/";

  constructor(private httpClient:HttpClient,
    private localStorageService:LocalStorageService) { }

  login(loginModel:LoginModel){
    return this.httpClient.post<SingleResponseModel<TokenModel>>(this.apiUrl+"login",loginModel)
  }

  signup(signupModel:RegisterModel){
    return this.httpClient.post<SingleResponseModel<TokenModel>>(this.apiUrl+"register",signupModel)
  }

  isAuthenticated(){
    if(this.localStorageService.get("token")){
      return true;
    }else{
      return false;
    }
  }


}
