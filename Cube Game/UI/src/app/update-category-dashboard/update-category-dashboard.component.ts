import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DashboardService } from 'src/Services/dashboard.service';

@Component({
  selector: 'app-update-category-dashboard',
  templateUrl: './update-category-dashboard.component.html',
  styleUrls: ['./update-category-dashboard.component.css']
})
export class UpdateCategoryDashboardComponent {
  data : any
  dataString:any

  constructor(public myService : DashboardService , private route: ActivatedRoute , private myRoute : Router){
    this.dataString = this.route.snapshot.paramMap.get('data');
    this.data = JSON.parse(this.dataString);
  }
  updateCategory = new FormGroup({
    name : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),

  });

  onSubmitdata()
  {
    if(this.updateCategory.valid){

      const c = Object.create(null);
      c.categoryName =  this.updateCategory.value.name
      this.myService.UpdateCategory(this.data.id , c).subscribe({
        next:(u)=>{
          this.myRoute.navigate(['dashboard'])
        }
      })

    }
  }

  get CategoryName() : FormControl{
    return this.updateCategory.get("name") as FormControl;
  }
}
