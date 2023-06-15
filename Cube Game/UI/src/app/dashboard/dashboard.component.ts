import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';
import { DashboardService } from 'src/Services/dashboard.service';
import { OrderService } from 'src/Services/order.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  // -------- Category ------------
  AllCategories : any
  AllProduct : any
  AllOrders : any
  constructor(public myService : DashboardService ,
    public auth : AuthService , private router: Router ,
    public orderService : OrderService){}
  ngOnInit(): void {

    this.myService.GetAllCategories().subscribe({
      next:(data)=>{

         this.AllCategories = data
      },
      error:(err)=>{console.log(err)}
    })


    this.myService.GetAllProducts().subscribe({
      next:(data)=>{

         this.AllProduct = data
      },
      error:(err)=>{console.log(err)}
    })

    this.orderService.GetAllOrder().subscribe({
      next:(data)=>{

         this.AllOrders = data
      },
      error:(err)=>{console.log(err)}
    })
  }


  updatecategory(s : any) {
    this.router.navigate(['/update-category-dashboard', JSON.stringify(s)]);
  }

  deletecategory(id : any){
    this.myService.deleteCategory(id).subscribe({
      next:(u)=>{
       this.ngOnInit()
      }
    })
 }

// -------- Product ------------

updateproduct(s : any) {
  this.router.navigate(['/update-product-dashboard', JSON.stringify(s)]);
}

deleteproduct(id : any){
  this.myService.deleteProduct(id).subscribe({
    next:(u)=>{
     this.ngOnInit()
    }
  })
}

//  Show Tables
 show1 : Boolean = false
 show2 : Boolean = false
 show3 : Boolean = false

 showC(){

  this.show1 = true
  this.show2 = false
  this.show3 = false
 }

 showP(){
  this.show1 = false
  this.show2 = true
  this.show3 = false
 }

 showI(){
  this.show1 = false
  this.show2 = false
  this.show3 = true
 }

 Logout(){
    this.auth.logOut();
    this.router.navigate(['Browse'])
    window.location.reload()
 }
}

