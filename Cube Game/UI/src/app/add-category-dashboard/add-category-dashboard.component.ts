import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DashboardService } from 'src/Services/dashboard.service';

@Component({
  selector: 'app-add-category-dashboard',
  templateUrl: './add-category-dashboard.component.html',
  styleUrls: ['./add-category-dashboard.component.css']
})
export class AddCategoryDashboardComponent {
  constructor(public myService : DashboardService , private route: Router){}

  AddCategory = new FormGroup({
    name : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
  });

  onSubmitdata()
  {
    if(this.AddCategory.valid){

      const c = Object.create(null);
      c.categoryName =  this.AddCategory.value.name
      this.myService.addCategory(c).subscribe({
        next:()=>{
          this.route.navigate(['dashboard'])
        }
      })

    }
  }

  get CategoryName() : FormControl{
    return this.AddCategory.get("name") as FormControl;
  }
}

