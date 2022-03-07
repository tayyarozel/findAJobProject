import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-navi',
  templateUrl: './navi.component.html',
  styleUrls: ['./navi.component.css']
})
export class NaviComponent implements OnInit {

  constructor( private localStorageService:LocalStorageService,private authService:AuthService, private router:Router) { }

  ngOnInit(): void {
  }
  isAuthenticated(){
    return this.authService.isAuthenticated()
  }
  logOut(){
    this.localStorageService.clean();
    this.router.navigate([""])
  }

}
