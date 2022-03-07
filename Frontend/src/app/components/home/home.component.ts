import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchForm:FormGroup;
  constructor(private formBuilder:FormBuilder,private toastrService:ToastrService,private router:Router) { }

  ngOnInit(): void {
    this.createSearchForm();
  }
  createSearchForm(){
    this.searchForm=this.formBuilder.group({
      search:["",Validators.required],
    })
  }

  search(){
    if(this.searchForm.valid){
      let search =this.searchForm.get('search').value;
      this.router.navigate(["jobs/"+search])

    }else{
      this.router.navigate(["jobs"])
    }
  }
}
