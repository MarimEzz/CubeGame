import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DashboardService } from 'src/Services/dashboard.service';

@Component({
  selector: 'app-add-product-dashboard',
  templateUrl: './add-product-dashboard.component.html',
  styleUrls: ['./add-product-dashboard.component.css']
})
export class AddProductDashboardComponent implements OnInit{

  selectedtype: string = '';
  platform: string[] = ['Windows', 'Linux', 'Mac'];
  selectedCategoryId!: number;
  AllCategories : any

  //  radio Button
  TF: boolean[] = [true, false];

  popular!: boolean
  most!: boolean
  sale!: boolean
  updated!: boolean
  release!: boolean
  top!: boolean
  soon!:boolean
constructor(public myService : DashboardService , private route: Router){}

ngOnInit(): void {

  this.myService.GetAllCategories().subscribe({
    next:(data)=>{

       this.AllCategories = data
    },
    error:(err)=>{console.log(err)}
  })
}
AddProduct = new FormGroup({
  productname : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
  productdes : new FormControl("" , [Validators.required]),
  productprice : new FormControl("" , [Validators.required]),
  productdiscount : new FormControl("" , [Validators.required]),
  productrate : new FormControl("" , [Validators.required]),
  categoryP : new FormControl("" , [Validators.required]),
  developer : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
  releasedate :  new FormControl("" , [Validators.required , Validators.max(Date.now())]),
  processor : new FormControl("" , [Validators.required]),
  ram : new FormControl("" , [Validators.required]),
  os : new FormControl("" , [Validators.required]),
  MPop : new FormControl("" ,  [Validators.required]),
  Mp : new FormControl("" ,  [Validators.required]),
  Gsale :new FormControl("" ,  [Validators.required]),
  up : new FormControl("" ,  [Validators.required]),
  rel : new FormControl("" ,  [Validators.required]),
  topS: new FormControl("" ,  [Validators.required]),
  Csoon: new FormControl("" ,  [Validators.required]),
});


onSubmitdata(){
  if(this.AddProduct.valid){

    const c = Object.create(null);
    c.productName =  this.AddProduct.value.productname
    c.description = this.AddProduct.value.productdes

    c.price =  this.AddProduct.value.productprice
    c.discount = this.AddProduct.value.productdiscount
    c.rate =  this.AddProduct.value.productrate

    c.categoryId = this.selectedCategoryId
    c.developerName =  this.AddProduct.value.developer
    c.releaseDate = this.AddProduct.value.releasedate

    c.processor =  this.AddProduct.value.processor
    c.ram = this.AddProduct.value.ram
    c.platform =  this.selectedtype

    c.isMostPopular = this.popular
    c.isMostPlayed = this.most
    c.isGameOnSale = this.sale

    c.isTopSeller = this.top
    c.isRecentlyUpdated = this.updated
    c.isNewRelease = this.release

    c.isComingSoon = this.soon
    console.log(typeof c.categoryId)
    this.myService.addProduct(c).subscribe()
    this.route.navigate(['product-image'])
  }
}
getTodayDate(): string {
  const today = new Date();
  const year = today.getFullYear();
  const month = today.getMonth() + 1;
  const day = today.getDate();
  return `${year}-${month < 10 ? '0' + month : month}-${day < 10 ? '0' + day : day}`;
}


}
