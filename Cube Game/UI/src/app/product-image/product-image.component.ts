import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';
import { DashboardService } from 'src/Services/dashboard.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product-image',
  templateUrl: './product-image.component.html',
  styleUrls: ['./product-image.component.css']
})
export class ProductImageComponent implements OnInit {
  res : any
  url = './assets/img/banner1Modified.png'
  file : any
  AllProduct : any
  selectedProductId!: number;

  constructor(public myService : DashboardService ,  private route: Router , public auth : AuthService){}
  ngOnInit(): void {
  this.myService.GetAllProductsWithoutImages().subscribe({
    next:(data)=>{

       this.AllProduct = data
    },
    error:(err)=>{console.log(err)}
  })
}

AddImage = new FormGroup({

  ProductI : new FormControl("" , [Validators.required]),

});

onSubmitdata(){
  if(this.AddImage.valid){
      let formData = new FormData();
      formData.append('file' , this.file);
      this.myService.AddImage(this.selectedProductId , formData).subscribe({
        next:(d)=>{
          this.url = d.imageURL
        }
      })

    // message
      Swal.fire('Yesss!', 'Image Added!', 'success')
  }
}

onFileSelected(event: any) {
  this.file = event.target.files[0];
  this.url = URL.createObjectURL(this.file);
  };

  Logout(){
    this.auth.logOut();
    this.route.navigate(['Browse'])
    window.location.reload()
  }
}




