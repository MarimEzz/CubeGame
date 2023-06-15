import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DashboardService } from 'src/Services/dashboard.service';

@Component({
  selector: 'app-update-product-dashboard',
  templateUrl: './update-product-dashboard.component.html',
  styleUrls: ['./update-product-dashboard.component.css']
})
export class UpdateProductDashboardComponent implements OnInit{
  data : any
  dataString:any

// data in form
  selectedtype: string = '';
  platform: string[] = ['Windows', 'Linux', 'Mac'];
  selectedCategoryId!: number;
  AllCategories : any

  TF: boolean[] = [true, false];

  popular!: boolean
  most!: boolean
  sale!: boolean
  updated!: boolean
  release!: boolean
  top!: boolean
  soon!:boolean

  constructor(public myService : DashboardService ,
    private route: ActivatedRoute , private myRoute : Router){
    this.dataString = this.route.snapshot.paramMap.get('data');
    this.data = JSON.parse(this.dataString);
    console.log(this.data)
  }
  ngOnInit(): void {
    this.myService.GetAllCategories().subscribe({
      next:(d)=>{

         this.AllCategories = d
      },
      error:(err)=>{console.log(err)}
    })
  }
  updateProduct = new FormGroup({
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

  onSubmitdata()
  {
    if(this.updateProduct.valid){

    const c = Object.create(null);
    c.productName =  this.updateProduct.value.productname
    c.description = this.updateProduct.value.productdes

    c.price =  this.updateProduct.value.productprice
    c.discount = this.updateProduct.value.productdiscount
    c.rate =  this.updateProduct.value.productrate

    c.categoryId = this.selectedCategoryId
    c.developerName =  this.updateProduct.value.developer
    c.releaseDate = this.updateProduct.value.releasedate

    c.processor =  this.updateProduct.value.processor
    c.ram = this.updateProduct.value.ram
    c.platform =  this.selectedtype

    c.isMostPopular = this.popular
    c.isMostPlayed = this.most
    c.isGameOnSale = this.sale

    c.isTopSeller = this.top
    c.isRecentlyUpdated = this.updated
    c.isNewRelease = this.release

    c.isComingSoon = this.soon
    console.log(c)
      this.myService.UpdateProduct(this.data.productId , c).subscribe({
        next:(u)=>{
          this.myRoute.navigate(['dashboard'])
        }
      })

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

