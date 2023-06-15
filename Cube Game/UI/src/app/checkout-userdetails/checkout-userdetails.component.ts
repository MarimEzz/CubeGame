import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { CartService, Product } from 'src/Services/cart.service';
import { CheckoutService } from 'src/Services/checkout.service';

@Component({
  selector: 'app-checkout-userdetails',
  templateUrl: './checkout-userdetails.component.html',
  styleUrls: ['./checkout-userdetails.component.css']
})
export class CheckoutUserdetailsComponent implements OnInit{

  public Total : number =0
  AllProductCart: Product[] = [];
  AllUserData: any

  constructor(
    private myRouter : Router,
    private toast : NgToastService ,
    private ch : CheckoutService,
    private myService : CartService
    ){}

    ngOnInit(): void {
      this.ch.getUserDetails().subscribe({
        next:(data)=>{
          this.AllUserData = data
        },
        error: (err) => {
          console.error('Error:', err);
        }
      })

      this.myService.GetCart().subscribe({
        next: (items) => {
          console.log('Cart items:', items);
          this.AllProductCart = items;
         items.forEach(element => {
         this.Total += element.priceAfterDiscount;
         });

        },
        error: (err) => {
          console.error('Error:', err);
        }
      });

    }
    CheckOut = new FormGroup({
      FirstName : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
      lastName : new FormControl("" , [Validators.required , Validators.minLength(2) , Validators.pattern("[a-zA-Z].*")]),
      Email : new FormControl("" , [Validators.required , Validators.email]),
      username : new FormControl("" , [Validators.required])
    });

    OnCheck(){

    }
}
